using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.NHibernate.Entity.UserAgg
{
    public class Name : ValueObject
    {
        public virtual string FirstName { get; private set; }
        public virtual string LastName { get; private set; }

        // for NHibernate (mandatory)
        public Name()
        {

        }

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
        }

    }
}
