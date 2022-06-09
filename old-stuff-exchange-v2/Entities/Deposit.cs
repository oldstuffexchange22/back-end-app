using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace old_stuff_exchange_v2.Entities
{
    [Table("Deposit")]
    public class Deposit
    {
        [Key]
        public Guid Id { get; set; }
        public string WalletElectricName { get; set; }
        public string Descripion { get; set; }
        public decimal Amount { get; set; }
        public decimal CoinExchange { get; set; }
        public decimal RemainingCoinInWallet { get; set; }
        public DateTime CreatedAt { get; set; }

        #region Relationship
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        #endregion

        public Deposit() {
            Transactions = new List<Transaction>();
        }

    }
}
