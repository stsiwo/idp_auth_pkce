using OrderingApi.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.Model
{
    public class OrderModel
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public UserModel User { get; set; }
        public ISet<OrderProductModel> OrderProducts { get; set; }
    }
}
