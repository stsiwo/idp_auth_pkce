using OrderingApiUnitTest.NHibernate.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.NHibernate.Entity.UserAgg
{
    public class UserId : GuidIdValueObject
    {
        public UserId() : base()
        {

        }

        public UserId(Guid id) : base(id)
        {

        }
    }
}
