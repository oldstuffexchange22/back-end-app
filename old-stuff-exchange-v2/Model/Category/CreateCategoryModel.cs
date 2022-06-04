using System;

namespace Old_stuff_exchange.Model.Category
{
    public class CreateCategoryModel
    {
        public string Name { get; set; }
        public Guid ParentId { get; set; }
    }
}
