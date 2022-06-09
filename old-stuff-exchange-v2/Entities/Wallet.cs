using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace old_stuff_exchange_v2.Entities
{
    [Table("Wallet")]
    public class Wallet
    {
        [Key]
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public string Desription { get; set; }

        #region Rellationship
        public Guid? UserId { get; set; }
        public User User { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        #endregion
        public Wallet() {
            Transactions = new List<Transaction>();
        }

    }
}
