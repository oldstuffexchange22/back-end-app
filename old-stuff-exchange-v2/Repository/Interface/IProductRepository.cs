using old_stuff_exchange_v2.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Repository.Interface
{
    public interface IProductRepository<T>
    {
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task<bool> Delete(Guid id);
    }
}
