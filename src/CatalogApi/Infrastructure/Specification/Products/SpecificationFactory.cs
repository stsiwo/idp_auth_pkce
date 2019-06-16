using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Specification.Products
{
    public class SpecificationFactory<P, T> : ISpecificationFactory<P, T>
        where T : ISpecification<Product>
    {
        private readonly Func<P, T> _factory;

        public SpecificationFactory(Func<P, T> factory)
        {
            _factory = factory;
        }

        public T Create(P param)
        {
            return _factory(param);
        }
    }
}
