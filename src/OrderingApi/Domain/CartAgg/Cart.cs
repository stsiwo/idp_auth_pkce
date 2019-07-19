﻿using OrderingApi.Domain.Base;
using OrderingApi.Domain.ProductAgg;
using OrderingApi.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.CartAgg
{
    public class Cart : IAggregateEntity
    {
        public CartId CartId { get; set; }
        public UserId UserId { get; set; }
        public IList<Product> Products { get; set; }
    }
}
