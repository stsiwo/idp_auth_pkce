using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Specification.Products
{
    public class SpecificationFactory<T> : ISpecificationFactory<T>
        where T : ISpecification<Product>
    {
        private readonly Func<string, T> _factory;

        public SpecificationFactory(Func<string, T> factory)
        {
            _factory = factory;
        }

        public T Create(string param)
        {
            return _factory(param);
        }
    }
}
