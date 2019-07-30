using OrderingApi.Domain.Base;
using OrderingApi.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.CartAgg
{
    public class Cart : IEntity, IAggregate
    {
        public virtual Guid Id { get; set; }
        public virtual User User { get; set; }
        public virtual ISet<CartProduct> Products { get; set; }

        public Cart()
        {
            Products = new HashSet<CartProduct>();
        }
    }
}
