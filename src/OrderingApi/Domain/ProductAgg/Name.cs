using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.ProductAgg
{
    public class Name : IValueObject
    {
        public readonly string FullName;

        public Name(string fullName)
        {
            FullName = fullName;
        }
    }
}
