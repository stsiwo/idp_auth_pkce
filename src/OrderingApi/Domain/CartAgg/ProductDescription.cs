using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.CartAgg
{
    public class ProductDescription : ValueObject
    {
        public virtual string FullDescription { get; set; }

        public ProductDescription()
        {

        }

        public ProductDescription(string fullDescription)
        {
            FullDescription = fullDescription;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FullDescription;
        }
    }
}
