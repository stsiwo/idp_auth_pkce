using Autofac.Features.Indexed;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Core;
using CatalogApi.Infrastructure.Specification.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogApiUnitTest.DI.TestComponents
{
    class SpecificationFactoryClient
    {
        IIndex<string, ISpecificationFactory<CategoryConstants, ISpecification<Product>>> _spFactory;

        ISpecification<Product> _sp;

        public SpecificationFactoryClient(IIndex<string, ISpecificationFactory<CategoryConstants, ISpecification<Product>>> spFactory)
        {
            _spFactory = spFactory; 
        }

        public void SetSpecification(string qConst, CategoryConstants type)
        {
            _sp = _spFactory[qConst].Create(type);
        }

        public string GetSpecificationType()
        {
            return _sp.GetType().ToString(); 
        }
    }
}
