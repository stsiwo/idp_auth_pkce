using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Specification.Product
{
    public class IncludeCategorySpecification : CompositeSpecification<DataEntity.Product>
    {
        private readonly CategoryConstants _CategoryType;

        public IncludeCategorySpecification(CategoryConstants categoryType)
        {
            this._CategoryType = categoryType;
        }
        public override Expression<Func<DataEntity.Product, bool>> ToExpression()
        {
            return product => product.SubCategory.Category.Id == this._CategoryType;
        }
    }
}
