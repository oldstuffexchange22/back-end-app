using System;

namespace old_stuff_exchange_v2.Model.Wallet
{
    public class UpdateWalletModel
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public string Desription { get; set; }

        public Guid? UserId { get; set; }
    }
}
