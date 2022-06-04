using old_stuff_exchange_v2.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Repository.Interface
{
    public interface ICategoryRepository<T>
    {
        Task<Category> Create(Category category);
        Task<Category> Update(Category category);
        Task<bool> Delete(Guid id);
        Task<Category> GetById(Guid id);
        Task<List<Category>> GetList(string filter, int page, int pageSize);
    }
}
