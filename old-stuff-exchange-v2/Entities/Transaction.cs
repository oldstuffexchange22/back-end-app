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
        // Finish or not
        public string Status { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public decimal Remaining { get; set; }
        public DateTime CreatedAt { get; set; }


        #region Relationship
        public Guid? WalletId { get; set; }
        [ForeignKey("WalletId")]
        public Wallet Wallet { get; set; }

        public Guid? PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
        #endregion
    }
}
