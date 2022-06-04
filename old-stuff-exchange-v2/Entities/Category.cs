using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace old_stuff_exchange_v2.Entities
{
    [Table("Category")]
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        #region Relationship
        public Guid? ParentId { get; set; }
        public Category Parent { get; set; }

        public ICollection<Category> CategoriesChildren { get; set; }
        #endregion

        public Category() { 
            CategoriesChildren = new List<Category>();
        }

    }
}
