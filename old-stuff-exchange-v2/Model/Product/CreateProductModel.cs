﻿using System;

namespace Old_stuff_exchange.Model.Product
{
    public class CreateProductModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal RequiredDeposit { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid PostId { get; set; }
    }
}
