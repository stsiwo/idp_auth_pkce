using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Specification.Products
{
    public class IncludeSubCategorySpecification : CompositeSpecification<Product>
    {
        private readonly SubCategoryConstants _SubCategoryType;

        public IncludeSubCategorySpecification(SubCategoryConstants subCategoryType)
        {
            this._SubCategoryType = subCategoryType;
        }
        public override Expression<Func<Product, bool>> ToExpression()
        {
            return product => product.SubCategory.Id == this._SubCategoryType;
        }
    }
}
