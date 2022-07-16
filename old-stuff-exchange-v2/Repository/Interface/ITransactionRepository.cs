using old_stuff_exchange_v2.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Repository.Interface
{
    public interface ITransactionRepository<T>
    {
        Task<Transaction> Create(Transaction transaction);
        Task<List<Transaction>> CreateList(List<Transaction> transactions);
        Task<Transaction> GetById(Guid Id);
        Task<bool> Delete(Guid Id);
        Task<List<Transaction>> GetByUserId(Guid UserId, int page, int pageSize);
        Task<List<Transaction>> GetByWalletId(Guid WalletId, int page, int pageSize);
    }
}
