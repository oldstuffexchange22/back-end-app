using Bogus;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Enum.Currency;
using old_stuff_exchange_v2.Enum.Post;
using old_stuff_exchange_v2.Enum.Role;
using old_stuff_exchange_v2.Enum.Transaction;
using old_stuff_exchange_v2.Enum.User;
using old_stuff_exchange_v2.Enum.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Old_stuff_exchange.Service
{
    public class DatabaseService
    {
        public AppDbContext _context;

        public DatabaseService(AppDbContext context)
        {
            _context = context;
        }

        public void GenerateDataWithBogus() {
            // Generate apartment
            Faker<Apartment> FakerAparment = new Faker<Apartment>()
                .RuleFor(a => a.Name, faker => faker.Lorem.Sentence(4))
                .RuleFor(a => a.Address, faker => faker.Lorem.Sentence(10));
            List<Apartment> apartments = FakerAparment.Generate(5);
            _context.Apartments.AddRange(FakerAparment);

            // Generate building
            Faker<Building> FakerBuilding = new Faker<Building>()
                .RuleFor(b => b.Name, faker => faker.Lorem.Sentence(3))
                .RuleFor(b => b.Apartment, faker => faker.PickRandom(apartments))
                .RuleFor(b => b.NumberFloor, faker => faker.Random.Int(3, 10))
                .RuleFor(b => b.NumberRoom, faker => faker.Random.Int(30, 100))
                .RuleFor(b => b.Description, faker => faker.Lorem.Sentence(6));
            List<Building> buildings = FakerBuilding.Generate(100);
            _context.Buildings.AddRange(buildings);

            // Generate role
            Role roleAdmin = new Role
            {
                Name = RoleNames.ADMIN,
            };
            Role roleResident = new Role
            {
                Name = RoleNames.RESIDENT,
            };
            List<Role> roles = new List<Role>();
            roles.Add(roleAdmin);
            roles.Add(roleResident);
            _context.AddRange(roles);

            // Generate user
            Faker fkUser = new Faker();
            User userAdmin = new User
            {
                UserName = "nvtan.a5@gmail.com",
                Email = "nvtan.a5@gmail.com",
                Status = UserStatus.ACTIVE,
                Role = roleResident,
                FullName = "nvtan.a5@gmail.com",
                Building = fkUser.PickRandom(buildings)
            };
            User userAdmin2 = new User
            {
                UserName = "admin",
                Email = "ADMIN@gmail.com",
                Status = UserStatus.ACTIVE,
                Role = roleAdmin,
                FullName = "admin",
                Password = "admin"
            };
            Faker<User> FakerUser = new Faker<User>()
                .RuleFor(u => u.UserName, faker => faker.Person.UserName)
                .RuleFor(u => u.Email, faker => faker.Person.Email)
                .RuleFor(u => u.Status, UserStatus.ACTIVE)
                .RuleFor(u => u.Role, roleResident)
                .RuleFor(u => u.FullName, faker => faker.Person.FullName)
                .RuleFor(u => u.Gender, faker => faker.PickRandom(UserGender.GetGenders()))
                .RuleFor(u => u.Building, faker => faker.PickRandom(buildings))
                .RuleFor(u => u.Phone, faker => faker.Person.Phone);
            _context.Add(userAdmin);
            _context.Add(userAdmin2);
            List<User> users = FakerUser.Generate(40);
            users.Add(userAdmin);
            _context.AddRange(users);

            // Generate wallet
            Wallet chairityWallet = new Wallet
            {
                Type = WalletType.CHAIRITY,
                Currency = Currency.XU,
                Status = WalletStatus.ACTIVE,
                UserId = null,
                Balance = 0
            };
            Wallet systemWallet = new Wallet
            {
                Type = WalletType.SYSTEM,
                Currency = Currency.XU,
                Status = WalletStatus.ACTIVE,
                UserId = null,
                Balance = 0
            };
            Faker<Wallet> FakerWalletDefault = new Faker<Wallet>()
                .RuleFor(w => w.Type, WalletType.DEFAULT)
                .RuleFor(w => w.Currency, Currency.XU)
                .RuleFor(w => w.Status, WalletStatus.ACTIVE)
                .RuleFor(w => w.Balance, faker => faker.Random.Int(500, 1000))
                .RuleFor(w => w.Desription, faker => faker.Lorem.Sentence(6));
            Faker<Wallet> FakerWalletPromotion = new Faker<Wallet>()
                .RuleFor(w => w.Type, WalletType.PROMOTION)
                .RuleFor(w => w.Currency, Currency.XU)
                .RuleFor(w => w.Status, WalletStatus.ACTIVE)
                .RuleFor(w => w.Balance, faker => faker.Random.Int(500, 1000))
                .RuleFor(w => w.Desription, faker => faker.Lorem.Sentence(6));
            List<Wallet> wallets = new List<Wallet>();
            wallets.Add(systemWallet);
            wallets.Add(chairityWallet);
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Role != roleAdmin) {
                    Wallet walletDefault = FakerWalletDefault.Generate();
                    Wallet walletPromotion = FakerWalletPromotion.Generate();
                    walletDefault.UserId = users[i].Id;
                    walletPromotion.UserId = users[i].Id;
                    List<Wallet> walletUserDefine = new List<Wallet>();
                    walletUserDefine.Add(walletDefault);
                    walletUserDefine.Add(walletPromotion);
                    wallets.AddRange(walletUserDefine);
                }
            }
            _context.Wallets.AddRange(wallets);

            // Generate category
            Faker<Category> FakerCategory = new Faker<Category>()
                .RuleFor(c => c.Name, faker => faker.Lorem.Sentence(3, 4))
                .RuleFor(c => c.Description, faker => faker.Lorem.Sentence(20));
            List<Category> categories = FakerCategory.Generate(4);
            _context.AddRange(categories);
            _context.SaveChanges();

            List<Category> parentCategories = _context.Categories.ToList();
            Faker<Category> FakerChilrenCategory = new Faker<Category>()
                .RuleFor(c => c.Name, faker => faker.Lorem.Sentence(3, 4))
                .RuleFor(c => c.Description, faker => faker.Lorem.Sentence(6))
                .RuleFor(c => c.Parent, faker => faker.PickRandom(parentCategories));
            List<Category> chilrenCategories = FakerChilrenCategory.Generate(10);
            _context.Categories.AddRange(chilrenCategories);
            _context.SaveChanges();

            Faker<Category> FakerChilrenOfChilrenCategory = new Faker<Category>()
                .RuleFor(c => c.Name, faker => faker.Lorem.Sentence(3, 4))
                .RuleFor(c => c.Description, faker => faker.Lorem.Sentence(6))
                .RuleFor(c => c.Parent, faker => faker.PickRandom(chilrenCategories));
            List<Category> chilrenOfChilrenCategories = FakerChilrenCategory.Generate(10);
            _context.Categories.AddRange(chilrenOfChilrenCategories);
            _context.SaveChanges();

            // Generate Post
            List<Guid> listUserId = new List<Guid>();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Role != roleAdmin) listUserId.Add(users[i].Id);
            }
            Faker<Post> FakerPost = new Faker<Post>()
                .RuleFor(p => p.Title, faker => faker.Lorem.Sentence(10))
                .RuleFor(p => p.Description, faker => faker.Lorem.Sentences(2))
                .RuleFor(p => p.Price, faker => faker.Random.Int(200, 500))
                .RuleFor(p => p.Status, faker => faker.PickRandom(PostStatus.ListPostSatus()))
                .RuleFor(p => p.Author, faker => faker.PickRandom(users.Take(20)))
                .RuleFor(p => p.UserBought, faker => faker.PickRandom(listUserId))
                .RuleFor(p => p.CreatedAt, faker => faker.Date.Between(new DateTime(2020,1,1), DateTime.Now));
            List<Post> posts = FakerPost.Generate(2000);
            _context.Posts.AddRange(posts);

            // Generate product
            List<Product> products = new List<Product>();
            Faker<Product> FakerProduct = new Faker<Product>()
                .RuleFor(p => p.Name, faker => faker.Lorem.Sentence(4))
                .RuleFor(p => p.Description, faker => faker.Lorem.Sentence(6))
                .RuleFor(p => p.Price, faker => faker.Random.Int(50, 100))
                .RuleFor(p => p.Status, faker => faker.Random.Int(20, 100) + "%")
                .RuleFor(p => p.Category, faker => faker.PickRandom(_context.Categories.ToList()));
            // .RuleFor(p => p.Post, faker => faker.PickRandom(posts));
            for (int i = 0; i < posts.Count; i++)
            {
                Product product1 = FakerProduct.Generate();
                Product product2 = FakerProduct.Generate();
                product1.PostId = posts[i].Id;
                product2.PostId = posts[i].Id;
                products.Add(product1);
                products.Add(product2);
            }
            _context.Products.AddRange(products);

            // Generate deposit
            Faker<Deposit> FakerDeposit = new Faker<Deposit>()
                .RuleFor(d => d.WalletElectricName, faker => faker.PickRandom(WalletElectricName.GetWalletNames()))
                .RuleFor(d => d.Description, faker => faker.Lorem.Sentence(6))
                .RuleFor(d => d.Amount, faker => faker.Random.Int(500, 3000) * 1000)
                .RuleFor(d => d.User, faker => faker.PickRandom(users.Where(u => u.Role != roleAdmin)));
            List<Deposit> deposits = FakerDeposit.Generate(500);
            _context.AddRange(deposits);

            // Generate transaction
            Faker<Transaction> FakerTransactionPost = new Faker<Transaction>()
                .RuleFor(t => t.Description, faker => faker.Lorem.Sentence(6))
                .RuleFor(t => t.Status, TransactionStatus.SUCCESS)
                .RuleFor(t => t.Type, faker => faker.PickRandom(TransactionType.GetTransactionPost()))
                .RuleFor(t => t.Balance, faker => faker.Random.Int(1000, 2000))
                .RuleFor(t => t.Wallet, faker => faker.PickRandom(wallets))
                .RuleFor(t => t.Post, faker => faker.PickRandom(posts))
                .RuleFor(t => t.CoinExchange, faker => faker.Random.Int(50, 200));
            Faker<Transaction> FakerTransactionDeposit = new Faker<Transaction>()
                .RuleFor(t => t.Description, faker => faker.Lorem.Sentence(6))
                .RuleFor(t => t.Status, TransactionStatus.SUCCESS)
                .RuleFor(t => t.Type, TransactionType.RECHARGE)
                .RuleFor(t => t.Balance, faker => faker.Random.Int(1000, 2000))
                .RuleFor(t => t.Wallet, faker => faker.PickRandom(wallets))
                .RuleFor(t => t.Deposit, faker => faker.PickRandom(deposits))
                .RuleFor(t => t.CoinExchange, faker => faker.Random.Int(50, 200));

            List<Transaction> transactions = new List<Transaction>();
            List<Transaction> transactionPost = FakerTransactionPost.Generate(150);
            List<Transaction> transactionDeposit = FakerTransactionDeposit.Generate(50);
            for (int i = 0; i < transactionPost.Count; i++)
            {
                Post post = transactionPost[i].Post;
                Faker faker = new Faker();
                transactionPost[i].CreatedAt = faker.Date.Between(post.CreatedAt, DateTime.Now);
            }
            transactions.AddRange(transactionPost);
            transactions.AddRange(transactionDeposit);
            _context.AddRange(transactions);
            _context.SaveChanges();
        }

        public void DeleteAllData() {
            _context.Products.RemoveRange(_context.Products);
            _context.Categories.RemoveRange(_context.Categories);
            _context.Transactions.RemoveRange(_context.Transactions);
            _context.Deposits.RemoveRange(_context.Deposits);
            _context.Posts.RemoveRange(_context.Posts);
            _context.Wallets.RemoveRange(_context.Wallets);
            _context.Users.RemoveRange(_context.Users);
            _context.Roles.RemoveRange(_context.Roles);
            _context.Buildings.RemoveRange(_context.Buildings);
            _context.Apartments.RemoveRange(_context.Apartments);
            _context.SaveChanges();
        }
            

    }
}
