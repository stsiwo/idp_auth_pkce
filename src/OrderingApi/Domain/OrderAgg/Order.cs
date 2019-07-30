using OrderingApi.Domain.Base;
using OrderingApi.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.OrderAgg
{
    public class Order : IEntity, IAggregate
    {
        public virtual Guid Id { get; set; }

        public virtual OrderStatusConstants Status { get; set; }

        public virtual User User { get; set; }

        public virtual ISet<OrderProduct> Products { get; set; }

        public Order()
        {
            Products = new HashSet<OrderProduct>();
        }
    }
}
