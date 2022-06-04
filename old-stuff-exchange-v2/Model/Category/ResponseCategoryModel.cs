using System;

namespace old_stuff_exchange_v2.Model.Category
{
    public class ResponseCategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
    }
}
