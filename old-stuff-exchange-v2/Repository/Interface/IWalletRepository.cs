using old_stuff_exchange_v2.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Repository.Interface
{
    public interface IWalletRepository<T>
    {
        Task<Wallet> Create(Wallet wallet);
        Task<Wallet> Update(Wallet wallet);
        Task<bool> UpdateList(List<Wallet> wallets);
        Task<bool> Delete(Guid id);
        Task<Wallet> FindById(Guid id);
        Task<List<Wallet>> FindByUserId(Guid userId);
        Task<Wallet> FindByType(string type);
        Task<Wallet> FindByUserIdWithType(Guid userId, string Type);
    }
}
