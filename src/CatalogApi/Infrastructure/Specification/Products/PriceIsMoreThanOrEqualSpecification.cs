using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Specification.Products
{
    public class PriceIsMoreThanOrEqualSpecification : CompositeSpecification<Product>
    {
        private readonly decimal _MaxPrice;

        public PriceIsMoreThanOrEqualSpecification(string maxPrice)
        {
            this._MaxPrice = Convert.ToDecimal(maxPrice);
        }
        public override Expression<Func<Product, bool>> ToExpression()
        {
            return product => product.Price >= this._MaxPrice;
        }
    }
}
