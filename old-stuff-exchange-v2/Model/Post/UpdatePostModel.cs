using System;

namespace Old_stuff_exchange.Model.Post
{
    public class UpdatePostModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Expired { get; set; }
        public string Status { get; set; }
        public string ImageUrl { get; set; }
    }
}
