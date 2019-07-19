using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.ProductAgg
{
    public class ProductId : IValueObject
    {
        public readonly string Id;

        public ProductId()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
