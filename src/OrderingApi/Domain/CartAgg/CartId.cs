using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.CartAgg
{
    public class CartId : IValueObject
    {
        public readonly string Id;

        public CartId()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
