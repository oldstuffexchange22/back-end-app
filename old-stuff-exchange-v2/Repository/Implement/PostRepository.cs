using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Old_stuff_exchange.Model;
using Old_stuff_exchange.Model.User;
using Old_stuff_exchange.Repository.Interface;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Entities.Extentions;
using old_stuff_exchange_v2.Enum.Post;
using old_stuff_exchange_v2.Enum.Sort;
using old_stuff_exchange_v2.Model;
using old_stuff_exchange_v2.Model.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Repository.Implement
{
    public class PostRepository : IPostRepository<Post>
    {
        private readonly AppDbContext _context;
        public PostRepository(AppDbContext context) { 
            _context = context;
        }

        public async Task<Post> AcceptPost(Guid id)
        {
            Post post =await _context.Posts.FindAsync(id);
            if (post == null || post.Status != PostStatus.WAITING) return null;
            post.Status = PostStatus.ACTIVE;
            int result = await _context.SaveChangesAsync();
            return result > 0 ? post : null;
        }
        public async Task<Post> NotAcceptPost(Guid id)
        {
            Post post = await _context.Posts.FindAsync(id);
            if (post == null) return null;
            post.Status = PostStatus.INACTIVE;
            post.LastUpdatedAt = DateTime.Now;
            int result = await _context.SaveChangesAsync();
            return result > 0 ? post : null;
        }


        public async Task<Post> Create(Post post)
        {
            await _context.AddAsync(post);
            int result = await _context.SaveChangesAsync();
            return result > 0 ? post : null;
        }

        public async Task<bool> Delete(Guid id)
        {
            Post post = _context.Posts.SingleOrDefault(p => p.Id == id);
            post.Status = PostStatus.INACTIVE;
            // _context.Posts.Remove(post);
            _context.Posts.Update(post);
            int result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<ResponsePostModel>> GetList(Guid? exceptUserId,Guid? apartmentId, Guid? categoryId, PagingModel model)
        {
            var allPost = _context.Posts.Include(p => p.Author).ThenInclude(u => u.Building)
                                        .Include(p => p.Products).AsQueryable();
            var category = _context.Categories.Include(c => c.CategoriesChildren)
                                                .ThenInclude(c => c.CategoriesChildren)
                                                .ThenInclude(c => c.CategoriesChildren)
                                                .ThenInclude(c => c.CategoriesChildren).Where(c => c.Id == categoryId).SingleOrDefault();
            if (category != null) {
                listCategoriesId.Clear();
                GetListCatChildId(category);
            }
            model.FilterValue = model.FilterValue?.ToUpper();
            model.FilterWith = model.FilterWith?.ToUpper();
            model.Status = model.Status?.ToUpper();
            #region Filtering
            if (exceptUserId != null) {
                allPost = allPost.Where(post => post.AuthorId != exceptUserId);
            }
            if (apartmentId != null)
            {
                allPost = allPost.Where(post => post.Author.Building.ApartmentId == apartmentId);
            }
            if (categoryId != null)
            {
                listCategoriesId.Add(categoryId);
                allPost = allPost.Where(post => post.Products.Any(pro => listCategoriesId.Contains(pro.CategoryId)));
            }
            if (!string.IsNullOrEmpty(model.FilterWith) && !string.IsNullOrEmpty(model.FilterValue))
            {
                model.FilterWith = model.FilterWith.ToUpper();
                switch (model.FilterWith)
                {
                    case "TITLE": allPost = allPost.Where(p => p.Title.ToUpper().Contains(model.FilterValue.ToUpper())); break;
                    case "DESCRPTION": allPost = allPost.Where(p => p.Description.ToUpper().Contains(model.FilterValue.ToUpper())); break;
                }
            }
            if (!string.IsNullOrEmpty(model.Status)) {
                    allPost = allPost.Where(p => p.Status.Equals(model.Status));
                }
            #endregion
            #region Sorting
            if (!string.IsNullOrEmpty(model.SortBy) && !string.IsNullOrEmpty(model.SortType))
            {
                switch (model.SortBy)
                {
                    case "CREATEAT":
                        switch (model.SortType)
                        {
                            case SortType.ASCENDING: allPost = allPost.OrderBy(p => p.CreatedAt); break;
                            case SortType.DESCENDING: allPost = allPost.OrderByDescending(p => p.CreatedAt); break;
                            default: allPost = allPost.OrderByDescending(p => p.CreatedAt); break;
                        }
                        break;
                    default: allPost = allPost.OrderByDescending(p => p.CreatedAt); break;
                }
            }
            else { // default order descending by create time
                allPost = allPost.OrderByDescending(p => p.CreatedAt);
            }
            #endregion
            #region Paging
            var result = PaginatedList<Post>.Create(allPost, model.Page, model.PageSize);
            #endregion
            #region convert response model
            List<ResponsePostModel> response = result.ToList().ConvertAll<ResponsePostModel>(post => post.ToResponseModel());
            int size = response.Count;
            for (int i = 0; i < size; i++)
            {
                if (response[i].UserBought != null) {
                    response[i].UserBoughtObject = _context.Users.Find(response[i].UserBought).ToResponseModel();
                }
            }
            #endregion
            return await Task.FromResult(response);

        }


        private List<Guid?> listCategoriesId = new List<Guid?>();
        private void GetListCatChildId(Category category) {
            for (int i = 0; i < category.CategoriesChildren.Count; i++)
            {
                Guid id = category.CategoriesChildren.ToList()[i].Id;
                listCategoriesId.Add(id);
                if (category.CategoriesChildren.ToList()[i].CategoriesChildren.Count > 0) {
                    if (category.CategoriesChildren.ToList()[i] != null) GetListCatChildId(category.CategoriesChildren.ToList()[i]);
                }
            }
        }

        public async Task<List<ResponsePostModel>> GetListByUserId(Guid userId,string title, string status, int page, int pageSize, bool isOrderLastUpdate)
        {
            User user = await _context.Users.FindAsync(userId);
            if (user == null) return null;
            IQueryable<Post> allPost = _context.Posts.Where(p => p.AuthorId == userId).Include(p => p.Author).AsQueryable();
            #region Filtering
            if (!string.IsNullOrEmpty(status)) allPost = allPost.Where(p => p.Status.ToUpper().Equals(status.ToUpper()));
            if (!string.IsNullOrEmpty(title)) allPost = allPost.Where(p => p.Title.ToUpper().Contains(title.ToUpper()));
            #endregion
            #region Sorting and Paging
            if (isOrderLastUpdate)
            {
                allPost = allPost.OrderByDescending(p => p.LastUpdatedAt);
            }
            else {
                allPost = allPost.OrderByDescending(p => p.CreatedAt);
            }
            var result = PaginatedList<Post>.Create(allPost, page, pageSize);
            #endregion
            #region convert response model
            List<ResponsePostModel> response = result.ToList().ConvertAll<ResponsePostModel>(post => post.ToResponseModel());
            int size = response.Count;
            for (int i = 0; i < size; i++)
            {
                if (response[i].UserBought != null)
                {
                    response[i].UserBoughtObject = _context.Users.Include(u => u.Building).FirstOrDefault(u => u.Id == response[i].UserBought).ToResponseModel();
                }
            }
            #endregion
            return await Task.FromResult(response);
        }

        public async Task<Post> GetPostById(Guid id)
        {
            Post post = _context.Posts.Include(p => p.Products)
                .Include(p => p.Author).AsNoTracking().SingleOrDefault(p => p.Id == id);
         /*   ResponsePostModel response = post.ToResponseModel();
            if (post.UserBought != null) { 
                ResponseUserModel user = _context.Users.Include(u => u.Building).FirstOrDefault(u => u.Id == post.UserBought).ToResponseModel();
                response.UserBoughtObject = user;
            }*/

            return await Task.FromResult(post);
        }

        public async Task<ResponsePostModel> GetPostByIdResponseModel(Guid id)
        {
            Post post = _context.Posts.Include(p => p.Products)
                .Include(p => p.Author).ThenInclude(u => u.Building).AsNoTracking().SingleOrDefault(p => p.Id == id);
            ResponsePostModel response = post.ToResponseModel();
            if (post.UserBought != null)
            {
                ResponseUserModel user = _context.Users.Include(u => u.Building).FirstOrDefault(u => u.Id == post.UserBought).ToResponseModel();
                response.UserBoughtObject = user;
            }

            return await Task.FromResult(response);
        }

        public async Task<Post> Update(Post post)
        {
            Post postDb = _context.Posts.AsNoTracking().SingleOrDefault(p => p.Id == post.Id);
            if (postDb != null) {
                post.CreatedAt = postDb.CreatedAt;
                post.AuthorId = postDb.AuthorId;
                post.LastUpdatedAt = DateTime.UtcNow;
            }
            _context.Posts.Update(post);
            int result = await _context.SaveChangesAsync();
            return result > 0 ? post : null;
        }

        public async Task<List<ResponsePostModel>> GetPostByUserBought(Guid userId, string status, int page, int pageSize)
        {
            User user = await _context.Users.FindAsync(userId);
            if (user == null) return null;
            IQueryable<Post> allPost = _context.Posts.Where(p => p.UserBought == userId).Include(p => p.Author).AsQueryable();
            #region Filtering
            if (!string.IsNullOrEmpty(status)) allPost = allPost.Where(p => p.Status.ToUpper().Equals(status.ToUpper()));
            #endregion
            #region Sorting and Paging
            allPost = allPost.OrderByDescending(p => p.LastUpdatedAt);
            var result = PaginatedList<Post>.Create(allPost, page, pageSize);
            #endregion
            #region convert response model
            List<ResponsePostModel> response = result.ToList().ConvertAll<ResponsePostModel>(post => post.ToResponseModel());
            int size = response.Count;
            for (int i = 0; i < size; i++)
            {
                if (response[i].UserBought != null)
                {
                    response[i].UserBoughtObject = _context.Users.Find(response[i].UserBought).ToResponseModel();
                }
            }
            #endregion
            return await Task.FromResult(response);
        }

        
    }
}
