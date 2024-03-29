﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace old_stuff_exchange_v2.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        [MaxLength(12)]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string ImagesUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        #region Relationship
        public Guid RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public Guid? BuildingId { get; set; }
        [ForeignKey("BuildingId")]
        public Building Building { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Wallet> Wallets { get; set; }
        #endregion
        public User() {
            Wallets = new List<Wallet>();
            Posts = new List<Post>();
        }
    }
}
