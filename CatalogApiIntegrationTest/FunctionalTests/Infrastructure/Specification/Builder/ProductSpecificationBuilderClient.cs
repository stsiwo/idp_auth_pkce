using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Builder;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CatalogApiIntegrationTest.FunctionalTests.Infrastructure.Specification.Builder
{
    public class ProductSpecificationBuilderClient
    {
        private readonly ISpecificationBuilder<Product> _spBuilder;

        public ProductSpecificationBuilderClient(ISpecificationBuilder<Product> spBuilder)
        {
            _spBuilder = spBuilder;
        }

        public Expression<Func<Product, bool>> Build(NameValueCollection qs)
        {
            return _spBuilder.Build(qs);
        }
    }
}
