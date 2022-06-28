using Old_stuff_exchange.Repository.Interface;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Enum.Transaction;
using old_stuff_exchange_v2.Enum.Wallet;
using old_stuff_exchange_v2.Model.Deposit;
using old_stuff_exchange_v2.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Service
{
    public class DepositService
    {
        private readonly AppDbContext _context;
        private readonly IDepositRepository<Deposit> _repoDeposit;
        private readonly IUserRepository<User> _repoUser;
        private readonly ITransactionRepository<Transaction> _repoTransaction;
        private readonly IWalletRepository<Wallet> _repoWallet;
        public DepositService(IDepositRepository<Deposit> repoDeposit, IUserRepository<User> repoUser, 
            ITransactionRepository<Transaction> repoTransaction, IWalletRepository<Wallet> repoWallet,
            AppDbContext context)
        {
            _repoDeposit = repoDeposit;
            _repoUser = repoUser;
            _repoTransaction = repoTransaction;
            _repoWallet = repoWallet;
            _context = context;
        }
        public async Task<Deposit> Create(CreateDepositModel model) {
            Deposit deposit = await _repoDeposit.Create(new Deposit
            {
                WalletElectricName = model.WalletElectricName,
                Description = model.Descripion,
                Amount = model.Amount,
                UserId = model.UserId,
            }) ;
            int coinExchange = Convert.ToInt32(model.Amount / 1000);
            List<Wallet> userWallets = await _repoWallet.FindByUserId(model.UserId);
            Wallet defaultWallet = userWallets.SingleOrDefault(w => w.Type == WalletType.DEFAULT);
            Wallet promotionWallet = userWallets.SingleOrDefault(w => w.Type == WalletType.PROMOTION);
            Wallet chairityWallet = await _repoWallet.FindByType(WalletType.CHAIRITY); 
            // with 100 coin recharge: default 90 coin, promotion 10 coin, chairity 10 coin

            // default wallet
            defaultWallet.Balance = defaultWallet.Balance + (coinExchange * 9) / 10;  // 90% 
            Transaction defaultTransaction = new Transaction
            {
                Description = "RECHARGE from " + model.WalletElectricName,
                Status = TransactionStatus.SUCCESS,
                Type = TransactionType.RECHARGE,
                CoinExchange = (coinExchange * 9) / 10,
                Balance = defaultWallet.Balance + (coinExchange * 9) / 10,
                WalletId = defaultWallet.Id,
                DepositId = deposit.Id
            };
            // promotion wallet
            promotionWallet.Balance = promotionWallet.Balance + coinExchange / 10;
            Transaction promotionTransaction = new Transaction
            {
                Description = "RECHARGE from " + model.WalletElectricName + " promotion",
                Status = TransactionStatus.SUCCESS,
                Type = TransactionType.RECHARGE,
                CoinExchange = coinExchange / 10,
                Balance = promotionWallet.Balance + coinExchange / 10,
                WalletId = promotionWallet.Id,
                DepositId = deposit.Id
            };
            // chairity wallet
            chairityWallet.Balance = chairityWallet.Balance + coinExchange / 10;
            Transaction chairityTransaction = new Transaction
            {
                Description = "RECHARGE from " + model.WalletElectricName + " chairity",
                Status = TransactionStatus.SUCCESS,
                Type = TransactionType.RECHARGE,
                CoinExchange = coinExchange / 10,
                Balance = chairityWallet.Balance + coinExchange / 10,
                WalletId = chairityWallet.Id,
                DepositId = deposit.Id
            };

            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(defaultTransaction);
            transactions.Add(promotionTransaction);
            transactions.Add(chairityTransaction);

            List<Wallet> wallets = new List<Wallet>();
            wallets.Add(defaultWallet);
            wallets.Add(promotionWallet);
            wallets.Add(chairityWallet);
            
            await _repoTransaction.CreateList(transactions);
            await _repoWallet.UpdateList(wallets);

            return await _repoDeposit.GetById(deposit.Id);
        }

        public async Task<Deposit> GetById(Guid id) { 
            return await _repoDeposit.GetById(id);
        }

        public async Task<List<Deposit>> GetListByUserId(Guid userId, int page, int pageSize) { 
            return await _repoDeposit.GetList(userId, page, pageSize);
        }
    }
}
