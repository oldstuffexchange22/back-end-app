using System;

namespace Old_stuff_exchange.Model.Post
{
    public class CreatePostModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
    }
}
