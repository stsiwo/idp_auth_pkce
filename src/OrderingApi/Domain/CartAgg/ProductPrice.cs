using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.CartAgg
{
    public class ProductPrice : ValueObject
    {
        public virtual decimal StandardPrice { get; set; }

        public ProductPrice()
        {

        }

        public ProductPrice(decimal standardPrice)
        {
            StandardPrice = standardPrice;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return StandardPrice;
        }
    }
}
