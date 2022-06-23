using System;

namespace old_stuff_exchange_v2.Model.Transaction
{
    public class ResponseTransactionModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public decimal CoinExchange { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        #region Relationship
        public Guid? WalletId { get; set; }
        public Guid? PostId { get; set; }
        public Guid? DepositId { get; set; }
        #endregion
    }
}
