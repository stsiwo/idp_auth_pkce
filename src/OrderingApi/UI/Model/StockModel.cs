using OrderingApi.Domain.OrderAgg;
using OrderingApi.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.Model
{
    public class StockModel
    {
        public int CurrentStock { get; set; }
        public int AvailableStock { get; set; }
    }
}
