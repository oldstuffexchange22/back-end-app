using old_stuff_exchange_v2.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Repository.Interface
{
    public interface IDepositRepository<T>
    {
        Task<Deposit> Create(Deposit deposit);
        Task<Deposit> GetById(Guid Id);
        Task<List<Deposit>> GetList(Guid userId, int page, int pageSize);
    }
}
