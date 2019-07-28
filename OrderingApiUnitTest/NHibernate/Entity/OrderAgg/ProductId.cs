using OrderingApiUnitTest.NHibernate.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.NHibernate.Entity.OrderAgg
{
    public class ProductId : GuidIdValueObject
    {
        
        public ProductId() : base()
        {

        }

        public ProductId(Guid id) : base(id)
        {

        }


    }
}
