using old_stuff_exchange_v2.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Repository.Interface
{
    public interface IPostRepository<T>
    {
        Task<Post> Create(Post post);
        Task<Post> Update(Post post);
        Task<bool> Delete(Guid id);
        Task<Post> GetPostById(Guid id);
    }
}
