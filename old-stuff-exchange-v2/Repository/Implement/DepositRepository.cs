using Old_stuff_exchange.Model;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Repository.Implement
{
    public class DepositRepository : IDepositRepository<Deposit>
    {
        private readonly AppDbContext _appDbContext;
        public DepositRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Deposit> Create(Deposit deposit)
        {
            _appDbContext.Deposits.Add(deposit);
            int result = await _appDbContext.SaveChangesAsync();
            return result > 0 ? deposit : null;
        }

        public async Task<Deposit> GetById(Guid Id)
        {
            Deposit deposit = _appDbContext.Deposits.Include(d => d.Transactions).Where(d => d.Id == Id).SingleOrDefault();
            return await Task.FromResult(deposit);
        }

        public async Task<List<Deposit>> GetList(Guid userId, int page, int pageSize)
        {
            IQueryable<Deposit> allDeposit = _appDbContext.Deposits.AsQueryable();
            #region Filtering
            allDeposit = allDeposit.Where(d => d.UserId == userId);
            #endregion
            #region Sorting
            allDeposit = allDeposit.OrderByDescending(d => d.CreatedAt);
            #endregion
            #region Paging
            var result = PaginatedList<Deposit>.Create(allDeposit, page, pageSize);
            #endregion
            return await Task.FromResult(result.ToList());
        }
    }
}