using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.OrderAgg
{
    public class OrderId
    {
        public readonly string Id;

        public OrderId()
        {
            Id = Guid.NewGuid().ToString();
        }


    }
}
