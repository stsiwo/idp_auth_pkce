using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.Model
{
    public class OrderProductModel
    {
        public string Id { get; set; }
        public ProductNameModel Name { get; set; }
        public PriceModel Price { get; set; }
        public StockModel Stock { get; set; }
        public ISet<OrderModel> Orders { get; set; } 
    }
}
