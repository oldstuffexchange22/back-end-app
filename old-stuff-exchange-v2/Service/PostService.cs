using Old_stuff_exchange.Model.Post;
using Old_stuff_exchange.Repository.Interface;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Enum.Post;
using old_stuff_exchange_v2.Model;
using old_stuff_exchange_v2.Repository.Interface;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using old_stuff_exchange_v2.Enum.Transaction;
using old_stuff_exchange_v2.Enum.Wallet;
using Old_stuff_exchange.Model.Product;

namespace Old_stuff_exchange.Service
{
    public class PostService
    {
        private readonly IPostRepository<Post> _postRepository;
        private readonly IUserRepository<User> _userRepository;
        private readonly ITransactionRepository<Transaction> _transationRepository;
        private readonly IWalletRepository<Wallet> _walletRepository;

        public PostService(IPostRepository<Post> postRepo, IUserRepository<User> userRepo, 
            ITransactionRepository<Transaction> transactionRepository, IWalletRepository<Wallet> walletRepository)
        {
            _postRepository = postRepo;
            _userRepository = userRepo;
            _transationRepository = transactionRepository;
            _walletRepository = walletRepository;
        }

        public async Task<Post> Create(CreatePostModel model) {
            Post post = new Post
            { 
                Id = string.IsNullOrEmpty(model.Id) ? Guid.NewGuid() : Guid.Parse(model.Id),
                Title = model.Title,
                Description = model.Description,
                AuthorId = model.AuthorId,
                Status = PostStatus.WAITING,
                ImageUrl = model.ImageUrl,
            };
            List<Product> products = model.Products.ConvertAll(p => new Product { 
            Name = p.Name, Description = p.Description, CategoryId = p.CategoryId, Price = p.Price, PostId = post.Id , Status = p.Status});
            post.Products = products;
            return await _postRepository.Create(post);
        }

        public async Task<Post> Update(UpdatePostModel model) {
            Post post = new Post
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Expired = model.Expired,
                Status = model.Status,
                LastUpdatedAt = DateTime.Now,
                ImageUrl = model.ImageUrl,
                Price = model.Price
            };
            return await _postRepository.Update(post);
        }

        public async Task<bool> Delete(Guid id) {
            return await _postRepository.Delete(id);
        }

        public async Task<List<Post>> GetList(Guid? exceptUserId,Guid? apartmentId, Guid? categoryId,PagingModel model) {
            return await _postRepository.GetList(exceptUserId,apartmentId, categoryId, model);
        }

        public async Task<Post> GetById(Guid id) {
            return await _postRepository.GetPostById(id);
        }

        public async Task<List<Post>> GetListByUserId(Guid userId, string status, int page, int pageSize) { 
            return await _postRepository.GetListByUserId(userId, status, page, pageSize);
        }

        public async Task<Post> AccepPost(Guid id) {
            return await _postRepository.AcceptPost(id);
        }

        public async Task<Post> NotAccepPost(Guid id)
        {
            return await _postRepository.NotAcceptPost(id);
        }

        public async Task<bool> BuyPost(Guid userId, Guid postId, string walletType) {
            User user = await _userRepository.GetById(userId);
            Post post = await _postRepository.GetPostById(postId);
            List<Wallet> walletsUser = await _walletRepository.FindByUserId(userId);
            Wallet userWallet = walletsUser.First(w => w.Type.ToUpper().Contains(walletType.ToUpper()));
            Wallet systemWallet = await _walletRepository.FindByType(WalletType.SYSTEM);
            decimal remainingCoin = userWallet.Balance - post.Price;
            if(user == null || post == null || walletsUser == null || systemWallet == null || 
                post.Status != PostStatus.ACTIVE || remainingCoin < 0) return await Task.FromResult(false);
            // transaction for system wallet
            Transaction systemTransaction = new Transaction
            {
                Description = user.Email + " buy the post id : " + post.Id,
                Status = TransactionStatus.SUCCESS,
                Type = TransactionType.BOUGHT,
                CoinExchange = post.Price,
                Balance = systemWallet.Balance + post.Price,
                WalletId = systemWallet.Id,
                PostId = post.Id,
            };

            // transaction for user bought wallet
            Transaction userTransaction = new Transaction
            {
                Description = user.Email + " buy the post id : " + post.Id,
                Status = TransactionStatus.SUCCESS,
                Type = TransactionType.BOUGHT,
                CoinExchange = post.Price,
                Balance = userWallet.Balance - post.Price,
                WalletId = userWallet.Id,
                PostId = post.Id,
            };

            userWallet.Balance = userWallet.Balance - post.Price;
            systemWallet.Balance = systemWallet.Balance + post.Price;
            List<Wallet> walletsUpdate = new List<Wallet>();
            walletsUpdate.Add(userWallet);
            walletsUpdate.Add(systemWallet);
            bool state = await _walletRepository.UpdateList(walletsUpdate);

            if(!state) return await Task.FromResult(false);

            post.Status = PostStatus.DELIVERY;
            post.UserBought = user.Id;
            await _postRepository.Update(post);

            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(systemTransaction);
            transactions.Add(userTransaction);
            await _transationRepository.CreateList(transactions);
            return true;
        }

        public async Task<Post> DeliveredPost(Guid id) {
            Post post = await _postRepository.GetPostById(id);
            if (post == null || post.Status != PostStatus.DELIVERY) return null;
            post.Status = PostStatus.DELIVERED;
            return await _postRepository.Update(post);
        }

        public async Task<Post> AccomplishedPost(Guid postId)
        {
            Post post = await _postRepository.GetPostById(postId);
            User userMakePost = await _userRepository.GetById(post.AuthorId);

            Wallet userWallet = await _walletRepository.FindByUserIdWithType(userMakePost.Id, WalletType.DEFAULT);
            Wallet systemWallet = await _walletRepository.FindByType(WalletType.SYSTEM);

            if (userMakePost == null || post == null || userWallet == null || systemWallet == null ||
                post.Status != PostStatus.DELIVERED) return null;
            // transaction for system wallet
            Transaction systemTransaction = new Transaction
            {
                Description = userMakePost.Email + " sell the post id : " + post.Id,
                Status = TransactionStatus.SUCCESS,
                Type = TransactionType.SELL,
                CoinExchange = post.Price,
                Balance = systemWallet.Balance - post.Price,
                WalletId = systemWallet.Id,
                PostId = post.Id,
            };

            // transaction for user sell wallet
            Transaction userTransaction = new Transaction
            {
                Description = userMakePost.Email + " sell the post id : " + post.Id,
                Status = TransactionStatus.SUCCESS,
                Type = TransactionType.SELL,
                CoinExchange = post.Price,
                Balance = userWallet.Balance + post.Price,
                WalletId = userWallet.Id,
                PostId = post.Id,
            };

            userWallet.Balance = userWallet.Balance + post.Price;
            systemWallet.Balance = systemWallet.Balance - post.Price;
            List<Wallet> walletsUpdate = new List<Wallet>();
            walletsUpdate.Add(userWallet);
            walletsUpdate.Add(systemWallet);
            bool state = await _walletRepository.UpdateList(walletsUpdate);

            if (!state) return null;

            post.Status = PostStatus.ACCOMPLISHED;
            await _postRepository.Update(post);

            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(systemTransaction);
            transactions.Add(userTransaction);
            await _transationRepository.CreateList(transactions);
            return post;
        }

        public async Task<Post> FailurePost(Guid postId)
        {
            Post post = await _postRepository.GetPostById(postId);
            User userBought = await _userRepository.GetById((Guid)post.UserBought);

            Wallet userWallet = await _walletRepository.FindByUserIdWithType(userBought.Id, WalletType.DEFAULT);
            Wallet systemWallet = await _walletRepository.FindByType(WalletType.SYSTEM);

            if (userBought == null || post == null || userWallet == null || systemWallet == null ||
                !(post.Status == PostStatus.DELIVERED || post.Status == PostStatus.DELIVERY)) return null;
            // transaction for system wallet
            Transaction systemTransaction = new Transaction
            {
                Description = userBought.Email + " refund the post id : " + post.Id,
                Status = TransactionStatus.SUCCESS,
                Type = TransactionType.REFUND,
                CoinExchange = post.Price,
                Balance = systemWallet.Balance - post.Price,
                WalletId = systemWallet.Id,
                PostId = post.Id,
            };

            // transaction for user bought wallet
            Transaction userTransaction = new Transaction
            {
                Description = userBought.Email + " sell the post id : " + post.Id,
                Status = TransactionStatus.SUCCESS,
                Type = TransactionType.REFUND,
                CoinExchange = post.Price,
                Balance = userWallet.Balance + post.Price,
                WalletId = userWallet.Id,
                PostId = post.Id,
            };

            userWallet.Balance = userWallet.Balance + post.Price;
            systemWallet.Balance = systemWallet.Balance - post.Price;
            List<Wallet> walletsUpdate = new List<Wallet>();
            walletsUpdate.Add(userWallet);
            walletsUpdate.Add(systemWallet);
            bool state = await _walletRepository.UpdateList(walletsUpdate);

            if (!state) return null;

            post.Status = PostStatus.FAILURE;
            await _postRepository.Update(post);

            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(systemTransaction);
            transactions.Add(userTransaction);
            await _transationRepository.CreateList(transactions);
            return post;
        }


    }
}
