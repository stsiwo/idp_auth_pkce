using OrderingApi.Domain.Base;
using OrderingApi.Domain.CartAgg;
using OrderingApi.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.CartAgg
{
    public class CartProduct : IEntity, IAggregate
    {
        public virtual Guid Id { get; set; }

        public virtual ProductName Name { get; set; }

        public virtual ProductDescription Description { get; set; }

        public virtual ProductMainImageUrl MainImageUrl { get; set; } 

        public virtual ProductPrice Price { get; set; }

        public virtual ProductStock Stock { get; set; }

        public virtual ISet<Cart> Carts { get; set; } 

        public CartProduct()
        {
            Carts = new HashSet<Cart>();
        }
    }
}
