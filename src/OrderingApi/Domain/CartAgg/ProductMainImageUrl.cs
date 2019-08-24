using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.CartAgg
{
    public class ProductMainImageUrl : ValueObject
    {
        public virtual string Url { get; set; }

        public ProductMainImageUrl()
        {

        }

        public ProductMainImageUrl(string url)
        {
            Url = url;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Url;
        }
    }
}
