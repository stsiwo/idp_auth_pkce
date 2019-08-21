using OrderingApi.Domain.Base;
using OrderingApi.Domain.CartAgg;
using OrderingApi.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.UserAgg
{
    public class User : EntityBase, IAggregate
    {
        public virtual Guid Id { get; set; }
        public virtual Name Name { get; set; } 
        public virtual Address HomeAddress { get; set; } 
        public virtual Phone Phone { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual ISet<Order> Orders { get; set; }

        public User()
        {
            Orders = new HashSet<Order>();
        }

    }
}
