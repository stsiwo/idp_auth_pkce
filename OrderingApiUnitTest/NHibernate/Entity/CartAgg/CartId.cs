using OrderingApiUnitTest.NHibernate.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.NHibernate.Entity.CartAgg
{
    public class CartId : GuidIdValueObject
    {
        public CartId() : base()
        {
            
        }

        public CartId(Guid id) : base(id) 
        {

        }
    }
}
