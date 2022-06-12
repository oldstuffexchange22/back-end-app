using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Model.Wallet;
using old_stuff_exchange_v2.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Service
{
    public class WalletService
    {
        private readonly IWalletRepository<Wallet> _walletRepo;
        public WalletService(IWalletRepository<Wallet> walletRepo)
        {
            _walletRepo = walletRepo;
        }
    
        public async Task<Wallet> Create(CreateWalletModel model)
        {
            Wallet wallet = new Wallet
            {
                Balance = model.Balance,
                Type = model.Type,
                Currency = model.Currency,
                Status = model.Status,
                Desription = model.Desription,
                UserId = model.UserId
            };
            return await _walletRepo.Create(wallet);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _walletRepo.Delete(id);
        }

        public async Task<Wallet> FindByType(string type)
        {
            return await _walletRepo.FindByType(type);
        }

        public async Task<List<Wallet>> FindByUserId(Guid userId)
        {
            return await _walletRepo.FindByUserId(userId);
        }

        public async Task<Wallet> FindByUserIdWithType(Guid userId, string Type)
        {
            return await _walletRepo.FindByUserIdWithType(userId, Type);
        }
      
        public async Task<Wallet> Update(UpdateWalletModel model)
        {
            Wallet wallet = await _walletRepo.FindById(model.Id);
            wallet.Balance = model.Balance;
            wallet.Type = model.Type;
            wallet.Currency = model.Currency;
            wallet.Status = model.Status;
            wallet.Desription = model.Desription;
            wallet.UserId = model.UserId;
            wallet.LastUpdatedAt = DateTime.Now;
            return await _walletRepo.Update(wallet);
        }

        public async Task<bool> UpdateList(List<Wallet> wallets)
        {
            return await _walletRepo.UpdateList(wallets);
        }

        public async Task<Wallet> FindById(Guid id)
        {
            return await _walletRepo.FindById(id);
        }
    }
}
