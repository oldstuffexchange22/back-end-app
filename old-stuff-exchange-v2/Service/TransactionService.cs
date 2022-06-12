using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Service
{
    public class TransactionService
    {
        private readonly ITransactionRepository<Transaction> _repo;
        public TransactionService(Repository.Interface.ITransactionRepository<Transaction> repo)
        {
            _repo = repo;
        }

        public async Task<Transaction> GetById(Guid id) {
            return await _repo.GetById(id);
        }

        public async Task<List<Transaction>> GetByUserId(Guid userId, int page, int pageSize) { 
            return await _repo.GetByUserId(userId, page, pageSize);
        }
    }
}
