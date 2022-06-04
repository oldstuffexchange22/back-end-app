using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace old_stuff_exchange_v2.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal RequiredDeposit { get; set; }
        public string StatusDeposit { get; set; }
        public string Status { get; set; }


        #region Relationship
        public Guid? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public Guid PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
        #endregion

    }
}
