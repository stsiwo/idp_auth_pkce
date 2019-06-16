using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Specification.Products
{
    public class PriceIsLessThanOrEqualSpecification : CompositeSpecification<Product>
    {
        private readonly decimal _MinPrice;

        public PriceIsLessThanOrEqualSpecification(string minPrice)
        {
            this._MinPrice = Convert.ToDecimal(minPrice);
        }
        public override Expression<Func<Product, bool>> ToExpression()
        {
            return product => product.Price <= this._MinPrice;
        }
    }
}
