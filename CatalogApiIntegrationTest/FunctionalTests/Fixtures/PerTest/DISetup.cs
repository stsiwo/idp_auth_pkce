using Autofac;
using CatalogApi.DI;
using CatalogApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApiIntegrationTest.FunctionalTests.Fixtures.PerTest
{
    public static class DISetup
    {
        public static ContainerBuilder GetAutofacContainerBuilder()
        {
            // use Autofac for DI and DbContext (implementation not mock) 
            var builder = new ContainerBuilder();

            // register module related to ProductQueryBuilder class
            builder.RegisterModule<ProductsControllerModule>();
            builder.RegisterModule<SpecificationModule>();
            builder.RegisterModule<ProductSpecificationFactoryModule>();
            builder.RegisterModule<OrderClauseStrategyModule>();
            builder.RegisterModule<SingletonModule>();
            builder.RegisterType<CatalogApiDbContext>().WithParameter("options", new DbContextOptionsBuilder<CatalogApiDbContext>().UseInMemoryDatabase("testDB").Options);

            return builder;
        }
    }
}
