using Old_stuff_exchange.Model.User;
using old_stuff_exchange_v2.Entities;
using System;

namespace old_stuff_exchange_v2.Model.Deposit
{
    public class ResponseDepositModel
    {
        public Guid Id { get; set; }
        public string WalletElectricName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public ResponseUserModel User { get; set; }
    }
}
