using old_stuff_exchange_v2.Model.Category;
using System;
using CategoryEntity = old_stuff_exchange_v2.Entities.Category;
using PostEntity = old_stuff_exchange_v2.Entities.Post;

namespace old_stuff_exchange_v2.Model.Product
{
    public class ResponseProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public ResponseCategoryModel Category { get; set; }
        public Guid PostId { get; set; }
    }
}
