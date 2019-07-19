using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.UserAgg
{
    public class Name : IValueObject
    {
        public readonly string FullName;

        public readonly string LastName;

        public Name(string fullName, string lastName)
        {
            FullName = fullName;
            LastName = lastName;
        }
    }
}
