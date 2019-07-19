using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.ProductAgg
{
    public class Stock : IValueObject
    {
        public readonly int CurrentStock;

        public Stock(int currentStock)
        {
            CurrentStock = currentStock;
        }
    }
}
