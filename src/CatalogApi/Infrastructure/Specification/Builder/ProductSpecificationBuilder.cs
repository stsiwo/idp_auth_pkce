using Autofac.Features.Indexed;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Core;
using CatalogApi.Infrastructure.Specification.Products;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Specification.Builder
{
    public class ProductSpecificationBuilder : SpecificationBuilder<Product>
    {
        IIndex<QueryStringConstants, ISpecificationFactory<ISpecification<Product>>> _specificationFactory;
        public ProductSpecificationBuilder(ISpecification<Product> specification) : base(specification)
        {

        }
        public override Func<Product, bool> Build(IDictionary<string, string> qs)
        {
            // 1. map query string dictionary with specification

            foreach (KeyValuePair<string, string> query in qs)
            {
                QueryStringConstants queryKey = QueryStringDictionary.Content[query.Key];

                ISpecification<Product> specification = _specificationFactory[queryKey].Create(query.Value);

                this._BaseSpecification.And(specification);
            }

            // 2. compile to Delegate
            return this._BaseSpecification.CompileToDelegate();

        }

    }
}
