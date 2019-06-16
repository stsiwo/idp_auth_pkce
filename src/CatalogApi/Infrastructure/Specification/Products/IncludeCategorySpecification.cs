using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Specification.Products
{
    public class IncludeCategorySpecification : CompositeSpecification<Product>
    {
        private readonly CategoryConstants _CategoryType;

        public IncludeCategorySpecification(string categoryType)
        {
            this._CategoryType = (CategoryConstants)categoryType;
        }
        public override Expression<Func<Product, bool>> ToExpression()
        {
            return product => product.SubCategory.Category.Id == this._CategoryType;
        }
    }
}
