using Microsoft.EntityFrameworkCore;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Repository.Implement
{
    public class WalletRepository : IWalletRepository<Wallet>
    {
        private readonly AppDbContext _appDbContext;
        public WalletRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Wallet> Create(Wallet wallet)
        {
            _appDbContext.Wallets.Add(wallet);
            int result =await _appDbContext.SaveChangesAsync();
            return result > 0 ? wallet : null;
        }

        public async Task<bool> Delete(Guid id)
        {
            Wallet wallet = await _appDbContext.Wallets.FindAsync(id);
            _appDbContext.Wallets.Remove(wallet);
            int result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<Wallet> FindById(Guid id)
        {
            Wallet wallet = _appDbContext.Wallets.SingleOrDefault(w => w.Id == id);
            return await Task.FromResult(wallet);
        }

        public async Task<Wallet> FindByType(string type)
        {
            Wallet wallet = _appDbContext.Wallets.SingleOrDefault(w => w.Type == type);
            return await Task.FromResult(wallet);
        }

        public async Task<List<Wallet>> FindByUserId(Guid userId)
        {
            List<Wallet> wallets = _appDbContext.Wallets.Where(w => w.UserId == userId).Include(w => w.User).ToList();
            return await Task.FromResult(wallets);
        }

        public async Task<Wallet> FindByUserIdWithType(Guid userId, string Type)
        {
            Wallet wallet = _appDbContext.Wallets.First(w => w.UserId == userId && w.Type.ToUpper().Equals(Type.ToUpper()));
            return await Task.FromResult(wallet);
        }

        public async Task<Wallet> Update(Wallet wallet)
        {
            _appDbContext.Update(wallet);
            int result =await _appDbContext.SaveChangesAsync();
            return result > 0 ? wallet : null;
        }

        public async Task<bool> UpdateList(List<Wallet> wallets)
        {
            _appDbContext.Wallets.UpdateRange(wallets);
            int result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }

     
    }
}
