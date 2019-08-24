using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Specification.Products
{
    // marker interface
    public interface ISpecificationFactory<out T> where T : ISpecification<Product>
    {
        T Create(string param);
    }
}
