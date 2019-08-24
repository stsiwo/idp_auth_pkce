using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.OrderAgg
{
    public class ProductName : ValueObject
    {
        public virtual string FullName { get; private set; }

        public ProductName()
        {

        }
        public ProductName(string fullName)
        {
            FullName = fullName;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FullName;
        }
    }
}
