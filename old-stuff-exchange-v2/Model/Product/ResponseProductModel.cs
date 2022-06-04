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
        public decimal RequiredDeposit { get; set; }
        public string StatusDeposit { get; set; }
        public string Status { get; set; }
        public CategoryEntity Category { get; set; }
        public PostEntity Post { get; set; }
    }
}
