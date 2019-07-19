using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.ProductAgg
{
    public class AvailableStock : IValueObject
    {
        public readonly int CurrentAvailableStock;

        public AvailableStock(int currentAvailableStock)
        {
            CurrentAvailableStock = currentAvailableStock;
        }
    }
}
