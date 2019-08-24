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
        IIndex<QueryStringConstants, ISpecificationFactory<ISpecification<Product>>> _spFactory;

        ISpecification<Product> _sp;

        public SpecificationFactoryClient(IIndex<QueryStringConstants, ISpecificationFactory<ISpecification<Product>>> spFactory)
        {
            _spFactory = spFactory; 
        }

        public void SetSpecification(QueryStringConstants qConst, string type)
        {
            _sp = _spFactory[qConst].Create(type);
        }

        public string GetSpecificationType()
        {
            return _sp.GetType().ToString(); 
        }
    }
}
