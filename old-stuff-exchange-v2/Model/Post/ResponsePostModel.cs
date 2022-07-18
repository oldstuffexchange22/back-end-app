﻿using Old_stuff_exchange.Model.User;
using System;

namespace old_stuff_exchange_v2.Model.Post
{
    public class ResponsePostModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Expired { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string Status { get; set; }
        public Guid AuthorId { get; set; }
        public ResponseUserModel Author { get; set; }
        public Guid? UserBought { get; set; }
        public ResponseUserModel UserBoughtObject { get; set; }
    }
}
