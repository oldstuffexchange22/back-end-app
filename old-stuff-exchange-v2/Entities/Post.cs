using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace old_stuff_exchange_v2.Entities
{
    [Table("Post")]
    public class Post
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Expired { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string Status { get; set; }

        #region Relationship
        public Guid AuthorId { get; set; }
        public User Author { get; set; }

        public ICollection<Product> Products { get; set; }
        // public ICollection<Transaction> Transactions { get; set; }
        #endregion

        public Post() {
            // Transactions = new List<Transaction>();
            Products = new List<Product>();
        }
    }
}
