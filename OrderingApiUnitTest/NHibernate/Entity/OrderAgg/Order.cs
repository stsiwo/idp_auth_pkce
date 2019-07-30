using OrderingApi.Domain.Base;
using OrderingApiUnitTest.NHibernate.Entity.UserAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.NHibernate.Entity.OrderAgg
{
    public class Order : IEntity, IAggregate
    {
        public virtual Guid Id { get; set; }

        public virtual OrderStatusConstants Status { get; set; }

        public virtual User User { get; set; }

        public virtual IList<Product> Products { get; set; }

        public Order()
        {
            Products = new List<Product>(); 
        }

        public Order(Guid id, OrderStatusConstants status, User user, IList<Product> products)
        {
            Id = id;
            Status = status;
            User = user;
            Products = products;
        }
    }
}
