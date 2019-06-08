using CatalogApi.Infrastructure.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Specification.Product
{
    public class PriceIsLessThanOrEqualSpecification : CompositeSpecification<DataEntity.Product>
    {
        private readonly decimal _MinPrice;

        public PriceIsLessThanOrEqualSpecification(decimal minPrice)
        {
            this._MinPrice = minPrice;
        }
        public override Expression<Func<DataEntity.Product, bool>> ToExpression()
        {
            return product => product.Price <= this._MinPrice;
        }
    }
}
