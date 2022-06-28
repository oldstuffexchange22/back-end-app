using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace old_stuff_exchange_v2.Entities
{
    [Table("Building")]
    public class Building
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? NumberFloor { get; set; }
        public int? NumberRoom { get; set; }
        public string Description { get; set; }

        #region Relationship
        public Guid ApartmentId { get; set; }
        [ForeignKey("ApartmentId")]
        public Apartment Apartment { get; set; }
        public ICollection<User> Users { get; set; }
        #endregion
        public Building() {
            Users = new List<User>();
        }
    }
}
