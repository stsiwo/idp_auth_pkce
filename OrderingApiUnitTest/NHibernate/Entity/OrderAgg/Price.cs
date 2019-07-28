using OrderingApiUnitTest.NHibernate.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.NHibernate.Entity.OrderAgg
{
    public class Price : ValueObject
    {
        public virtual decimal Amount { get; private set; }
        public virtual string Currency { get; private set; }

        // for NHibernate (mandatory)
        public Price()
        {

        }

        public Price(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return Currency;
        }

    }
}
