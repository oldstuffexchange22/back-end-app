using Old_stuff_exchange.Model.Post;
using Old_stuff_exchange.Repository.Interface;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Enum;
using System;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Service
{
    public class PostService
    {
        private readonly IPostRepository<Post> _repo;
        public PostService(IPostRepository<Post> repo)
        {
            _repo = repo;
        }

        public async Task<Post> Create(CreatePostModel model) {
            Post post = new Post
            {
                Title = model.Title,
                Description = model.Description,
                AuthorId = model.AuthorId,
                Status = PostStatus.WAITING
            };
            return await _repo.Create(post);
        }

        public async Task<Post> Update(UpdatePostModel model) {
            Post post = new Post
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Expired = model.Expired,
                Status = model.Status,
                LastUpdatedAt = DateTime.Now
            };
            return await _repo.Update(post);
        }

        public async Task<bool> Delete(Guid id) {
            return await _repo.Delete(id);
        }
    }
}
