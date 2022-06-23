using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace old_stuff_exchange_v2.Entities
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        #region Relationship
        public ICollection<UserResponseModel> Users { get; set; }
        #endregion
        public Role()
        {
            Users = new List<UserResponseModel>();
        }
    }
}
