using Microsoft.EntityFrameworkCore;
using Old_stuff_exchange.Repository.Interface;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Enum;
using System;
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

        public async Task<Post> GetPostById(Guid id)
        {
            Post post = _context.Posts.SingleOrDefault(p => p.Id == id);
            return await Task.FromResult(post);
        }

        public async Task<Post> Update(Post post)
        {
            Post postDb = _context.Posts.AsNoTracking().SingleOrDefault(p => p.Id == post.Id);
            if (postDb != null) {
                post.CreatedAt = postDb.CreatedAt;
                post.AuthorId = postDb.AuthorId;
            }
            _context.Posts.Update(post);
            int result = await _context.SaveChangesAsync();
            return result > 0 ? post : null;
        }
    }
}
