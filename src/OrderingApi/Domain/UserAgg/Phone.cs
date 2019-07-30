using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApi.Domain.UserAgg
{
    public class Phone : ValueObject
    {
        public virtual string HomeNumber { get; private set; }

        // for NHibernate (mandatory)
        public Phone()
        {

        }

        public Phone(string homeNumber)
        {
            HomeNumber = homeNumber;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return HomeNumber;
        }
    }
}
