using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.ProductAgg
{
    public class Description : IValueObject
    {
        public readonly string FullDescription;

        public Description(string fullDescription)
        {
            FullDescription = fullDescription;
        }
    }
}
