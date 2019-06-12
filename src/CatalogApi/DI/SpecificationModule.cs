using Autofac;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Builder;
using CatalogApi.Infrastructure.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.DI
{
    public class SpecificationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // ProductSpecificationBuilder
            builder.RegisterType<BaseSpecification<Product>>().As<ISpecification<Product>>().InstancePerLifetimeScope();
        }
    }
}
