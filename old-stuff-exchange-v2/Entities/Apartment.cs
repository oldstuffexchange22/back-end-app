using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace old_stuff_exchange_v2.Entities
{
    [Table("Apartment")]
    public class Apartment
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }

        #region Relationship
        public ICollection<Building> Buildings { get; set; }
        #endregion

        public Apartment()
        {
            Buildings = new List<Building>();
        }
    }
}
