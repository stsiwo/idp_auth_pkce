using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.NHibernate.Entity.OrderAgg
{
    public class Product : IEntity
    {
        public virtual ProductId Id { get; set; } 

        public virtual string Name { get; set; }

        public virtual Price Price { get; set; }

        public virtual IList<Order> Orders { get; set; }

        public Product()
        {
            Orders = new List<Order>(); 
        }
    }
}
