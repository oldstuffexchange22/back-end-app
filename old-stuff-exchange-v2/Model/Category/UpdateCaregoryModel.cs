using System;

namespace Old_stuff_exchange.Model.Category
{
    public class UpdateCaregoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }
}
