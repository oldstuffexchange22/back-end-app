using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace old_stuff_exchange_v2.Entities
{
    [Table("Transaction")]
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }


        #region Relationship
        public Guid? WalletId { get; set; }
        [ForeignKey("WalletId")]
        public Wallet Wallet { get; set; }

        public Guid? PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }

        public Guid? DepositId { get; set; }
        [ForeignKey("DepositId")]
        public Deposit Deposit { get; set; }
        #endregion
    }
}
