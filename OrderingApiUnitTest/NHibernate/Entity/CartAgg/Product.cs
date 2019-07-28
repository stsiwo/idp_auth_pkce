using OrderingApiUnitTest.NHibernate.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.NHibernate.Entity.CartAgg
{
    public class Product : IEntity
    {
        public virtual ProductId Id { get; set; } 
        public virtual string Name { get; set; }

        public virtual Price Price { get; set; }

        public virtual IList<Cart> Carts { get; set; }
        public Product()
        {
            Carts = new List<Cart>(); 
        }
    }
}
