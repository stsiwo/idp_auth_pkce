using Autofac.Features.Indexed;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Core;
using CatalogApi.Infrastructure.Specification.Products;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Specification.Builder
{
    public class ProductSpecificationBuilder : SpecificationBuilder<Product>
    {
        IIndex<QueryStringConstants, ISpecificationFactory<ISpecification<Product>>> _specificationFactory;
        public ProductSpecificationBuilder(ISpecification<Product> specification, IIndex<QueryStringConstants, ISpecificationFactory<ISpecification<Product>>> specificationFactory) : base(specification)
        {
            _specificationFactory = specificationFactory;

        }
        public override Expression<Func<Product, bool>> Build(NameValueCollection qs)
        {
            // 1. map query string dictionary with specification
            foreach (string key in qs)
            {
                QueryStringConstants queryKey = QueryStringDictionary.Content[key];

                ISpecification<Product> specification = _specificationFactory[queryKey].Create(qs[key]);

                this._BaseSpecification = this._BaseSpecification.And(specification);
            }

            // 2. compile to Delegate
            return this._BaseSpecification.ToExpression();

        }

    }
}
