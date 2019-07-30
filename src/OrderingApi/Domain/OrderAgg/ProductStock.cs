using OrderingApi.Domain.Base;
using System.Collections.Generic;

namespace OrderingApi.Domain.OrderAgg
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
