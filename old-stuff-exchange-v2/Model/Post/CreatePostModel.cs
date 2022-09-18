using Old_stuff_exchange.Model.Product;
using System;
using System.Collections.Generic;

namespace Old_stuff_exchange.Model.Post
{
    public class CreatePostModel
    {
        public string Id { get; set; } = null;
        public string Title { get; set; }
        // public string Status { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Guid AuthorId { get; set; }
        public List<CreateProductModel> Products { get; set; }
    }
}
