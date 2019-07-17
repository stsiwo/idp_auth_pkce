using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.OrderAgg
{
    public enum OrderStatusConstants
    {
       PENDING = 0,
       CONFIRMED_USER_INFO = 1,
       CONFIRMED_ORDER_PRODUCTS = 2,
       COMPLETED = 3
    }
}
