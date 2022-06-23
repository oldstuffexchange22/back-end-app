using Old_stuff_exchange.Model.User;
using old_stuff_exchange_v2.Entities;
using System;

namespace old_stuff_exchange_v2.Model.Wallet
{
    public class ResponseWalletModel
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public string Desription { get; set; }
        public ResponseUserModel User { get; set; }
    }
}
