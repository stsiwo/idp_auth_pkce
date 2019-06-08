using CatalogApi.Infrastructure.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Specification.Product
{
    public class PriceIsMoreThanOrEqualSpecification : CompositeSpecification<DataEntity.Product>
    {
        private readonly decimal _MaxPrice;

        public PriceIsMoreThanOrEqualSpecification(decimal maxPrice)
        {
            this._MaxPrice = maxPrice;
        }
        public override Expression<Func<DataEntity.Product, bool>> ToExpression()
        {
            return product => product.Price >= this._MaxPrice;
        }
    }
}
