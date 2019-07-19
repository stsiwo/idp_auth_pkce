﻿using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.UserAgg
{
    public class Address : IValueObject
    {
        public readonly string Street;
        public readonly string City;
        public readonly string State;
        public readonly string Country;
        public readonly string PostalCode;

        public Address(string street, string city, string state, string country, string postalCode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
        }
    }
}
