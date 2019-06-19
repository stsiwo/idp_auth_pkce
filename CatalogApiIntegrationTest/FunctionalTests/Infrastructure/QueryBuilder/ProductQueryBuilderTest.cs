using Autofac;
using Autofac.Features.Indexed;
using CatalogApi.DI;
using CatalogApi.Infrastructure;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.QueryBuilder;
using CatalogApi.Infrastructure.QueryBuilder.OrderClauseStrategy;
using CatalogApi.Infrastructure.Specification.Builder;
using CatalogApiIntegrationTest.TestData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Xunit;

namespace CatalogApiIntegrationTest.FunctionalTests.Infrastructure.QueryBuilder
{
    public class ProductQueryBuilderTest
    {
        /**
         * ProductQueryBuilder functinality tests 
         **/
        [Fact]
        public async void Build_IQueryable_ShouldConstructedWithoutWhereAndOrderByClause() 
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
            // 2.1. resolve dependency
            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // resolve context
                CatalogApiDbContext context = scope.Resolve<CatalogApiDbContext>();
                // 1.1. Ensure the database is created.
                context.Database.EnsureCreated();
                // 1.2. seed test data
                context.AddRange(ProductsGETEndpointTestData.GetProducts());
                await context.SaveChangesAsync();
                // expectedResult
                var expectedResult = ProductsGETEndpointTestData.GetProducts().Count;

                // 3. qs dummy
                NameValueCollection qsDummy = HttpUtility.ParseQueryString("");

                // act
                IQueryBuilder<Product> productQueryBuilder = scope.Resolve<IQueryBuilder<Product>>();
                var result = await productQueryBuilder.Build(qsDummy);
                var resultCount = result.Count;

                // assert
                Assert.Equal(expectedResult, resultCount);

            }

        }
    }
}
