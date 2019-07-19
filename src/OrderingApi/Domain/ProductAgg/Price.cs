using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.ProductAgg
{
    public class Price : IValueObject
    {
        public readonly decimal StandardPrice;

        public Price(decimal standardPrice)
        {
            StandardPrice = standardPrice;
        }
    }
}
