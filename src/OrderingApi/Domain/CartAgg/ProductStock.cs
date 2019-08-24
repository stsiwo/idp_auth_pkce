using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.CartAgg
{
    public class ProductStock : ValueObject
    {
        public virtual int CurrentStock { get; set; }
        public virtual int AvailableStock { get; set; }

        public ProductStock()
        {

        }
        public ProductStock(int currentStock, int availableStock)
        {
            CurrentStock = currentStock;
            AvailableStock = availableStock;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CurrentStock;
            yield return AvailableStock;
        }
    }
}
