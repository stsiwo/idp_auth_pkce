using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;

namespace OrderingApi.Domain.OrderAgg
{
    public class OrderProduct : IEntity, IAggregate
    {
        public virtual Guid Id { get; set; }

        public virtual ProductName Name { get; set; }

        public virtual ProductPrice Price { get; set; }

        public virtual ProductStock Stock { get; set; }

        public virtual ISet<Order> Orders { get; set; } 

        public OrderProduct()
        {
            Orders = new HashSet<Order>();
        }
    }
}
