using OrderingApi.Domain.Base;
using OrderingApiUnitTest.NHibernate.Entity.UserAgg;
using System;
using System.Collections.Generic;

namespace OrderingApiUnitTest.NHibernate.Entity.CartAgg
{
    public class Cart : IEntity, IAggregate
    {
        public virtual CartId Id { get; set; }
        public virtual User User { get; set; }
        public virtual IList<Product> Products { get; set; }

        public Cart()
        {
            Products = new List<Product>();
        }

        public Cart(CartId id, User user, IList<Product> products)
        {
            Id = id;
            User = user;
            Products = products;
        }
    }
}
