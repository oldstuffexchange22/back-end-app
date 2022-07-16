using System;

namespace old_stuff_exchange_v2.Model.Deposit
{
    public class CreateDepositModel
    {
        public string WalletElectricName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Guid UserId { get; set; }
    }
}
