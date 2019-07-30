using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApi.Domain.UserAgg 
{
    public class Address : ValueObject
    {
        public virtual string Street { get; private set; }
        public virtual string City { get; private set; }
        public virtual string State { get; private set; }
        public virtual string Country { get; private set; }
        public virtual string PostalCode { get; private set; }

        // for NHibernate (mandatory)
        public Address()
        {

        }

        public Address(string street, string city, string state, string country, string postalCode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return PostalCode;
        }
    }
}
