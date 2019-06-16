using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Specification.Products
{
    public class IncludeKeyWordSpecification : CompositeSpecification<Product>
    {
        private readonly String _KeyWord;

        public IncludeKeyWordSpecification(String keyword)
        {
            this._KeyWord = keyword;
        }
        public override Expression<Func<Product, bool>> ToExpression()
        {
            return product => product.Name.Contains(this._KeyWord) || product.Description.Contains(this._KeyWord);
        }
    }
}
