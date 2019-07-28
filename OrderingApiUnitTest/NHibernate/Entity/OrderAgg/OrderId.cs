using OrderingApiUnitTest.NHibernate.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.NHibernate.Entity.OrderAgg
{
    public class OrderId : GuidIdValueObject
    {
        public OrderId() : base()
        {

        }

        public OrderId(Guid id) : base(id)
        {

        }
    }
}
