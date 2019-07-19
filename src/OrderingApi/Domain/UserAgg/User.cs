using OrderingApi.Domain.Base;
using OrderingApi.Domain.CartAgg;
using OrderingApi.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.UserAgg
{
    public class User : IAggregateEntity
    {
        public UserId UserId { get; set; }
        public Name Name { get; set; } 
        public Address HomeAddress { get; set; } 
        public Phone Phone { get; set; }
        public CartId CartId { get; set; }
        public IList<OrderId> OrderIds { get; set; } 
    }
}
