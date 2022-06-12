using Microsoft.EntityFrameworkCore;
using Old_stuff_exchange.Model;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Repository.Implement
{
    public class TransactionRepository : ITransactionRepository<Transaction>
    {
        private readonly AppDbContext _context;
        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> Create(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            int result = await _context.SaveChangesAsync();
            return result > 0 ? transaction : null;
        }

        public async Task<List<Transaction>> CreateList(List<Transaction> transactions)
        {
            _context.Transactions.AddRange(transactions);
            int result = await _context.SaveChangesAsync();
            return result > 0 ? transactions : null;
        }

        public async Task<bool> Delete(Guid Id)
        {
            Transaction transaction = _context.Transactions.FirstOrDefault(t => t.Id == Id);
            if (transaction == null) return await Task.FromResult(false);
            _context.Transactions.Remove(transaction);
            int result = _context.SaveChanges();
            return result > 0;

        }

        public async Task<Transaction> GetById(Guid Id)
        {
            Transaction transaction = await _context.Transactions.FindAsync(Id);
            return transaction;
        }

        public async Task<List<Transaction>> GetByUserId(Guid UserId, int page, int pageSize)
        {
            IQueryable<Transaction> allTransactions = _context.Transactions.Include(t => t.Wallet).AsQueryable();

            #region Filtering
            allTransactions = allTransactions.Where(t => t.Wallet.UserId == UserId);
            #endregion

            #region Sorting
            allTransactions = allTransactions.OrderByDescending(t => t.CreatedAt);
            #endregion

            #region Paging
            var result = PaginatedList<Transaction>.Create(allTransactions, page, pageSize);
            #endregion
            return await Task.FromResult(result.ToList());
        }

    }
}
