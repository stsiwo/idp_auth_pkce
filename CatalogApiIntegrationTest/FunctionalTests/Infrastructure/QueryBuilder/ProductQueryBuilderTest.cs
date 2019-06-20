using Autofac;
using Autofac.Features.Indexed;
using CatalogApi.DI;
using CatalogApi.Infrastructure;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.QueryBuilder;
using CatalogApi.Infrastructure.QueryBuilder.OrderClauseStrategy;
using CatalogApi.Infrastructure.Specification.Builder;
using CatalogApiIntegrationTest.FunctionalTests.Fixtures.PerTest;
using CatalogApiIntegrationTest.TestData;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Xunit;
using Xunit.Abstractions;

namespace CatalogApiIntegrationTest.FunctionalTests.Infrastructure.QueryBuilder
{
    public class ProductQueryBuilderTest  
    {
        private ContainerBuilder _builder;
        private readonly ITestOutputHelper _output;

        public ProductQueryBuilderTest(ITestOutputHelper output)
        {
            _output = output;
            _builder = DISetup.GetAutofacContainerBuilder();

        }

        private async Task<CatalogApiDbContext> SetupInitialDB(CatalogApiDbContext context)
        {
            context.Database.EnsureCreated();
            // 1.2. seed test data
            context.AddRange(ProductsGETEndpointTestData.GetProducts());
            await context.SaveChangesAsync();

            return context;
        }
        /**
         * ProductQueryBuilder functinality tests 
         **/
        [Fact]
        public async void Build_IQueryable_ShouldConstructedWithoutWhereAndOrderByClause() 
        {
            // 2.1. resolve dependency
            using (var container = _builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // resolve context
                CatalogApiDbContext context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                context = await SetupInitialDB(context);
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

        [Fact]
        public async void Build_NoQueryString_ShouldReturnAllProductsWithDateAscOrder()
        {
            // 2.1. resolve dependency
            using (var container = _builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // resolve context
                CatalogApiDbContext context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                context = await SetupInitialDB(context);

                // 3. qs dummy
                string noQueryString = "";
                NameValueCollection qsDummy = HttpUtility.ParseQueryString("");

                // act
                IQueryBuilder<Product> productQueryBuilder = scope.Resolve<IQueryBuilder<Product>>();
                var products = await productQueryBuilder.Build(qsDummy);
                var result = products.Select(p => p.CreationDate).ToList();


                // assert
                Assert.True(result.Zip(result.Skip(1), (a, b) => new { a, b }).All(p => p.a.Date < p.b.Date));
            }
        }

        [Fact]
        public async void Build_DateAsc0SortQueryString_ShouldReturnAllProductsWithTheSortOrder()
        {
            // 2.1. resolve dependency
            using (var container = _builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // resolve context
                CatalogApiDbContext context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                context = await SetupInitialDB(context);

                // 3. qs dummy
                string dateAscSortQueryString = "?sort=0";
                NameValueCollection qsDummy = HttpUtility.ParseQueryString(dateAscSortQueryString);

                // act
                IQueryBuilder<Product> productQueryBuilder = scope.Resolve<IQueryBuilder<Product>>();
                var products = await productQueryBuilder.Build(qsDummy);
                var result = products.Select(p => p.CreationDate).ToList();

                // assert
                Assert.True(result.Zip(result.Skip(1), (a, b) => new { a, b }).All(p => p.a.Date < p.b.Date));
            }
        }

        [Fact]
        public async void Build_DateDesc1SortQueryString_ShouldReturnAllProductsWithTheSortOrder()
        {
            // 2.1. resolve dependency
            using (var container = _builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // resolve context
                CatalogApiDbContext context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                context = await SetupInitialDB(context);

                // 3. qs dummy
                string dateDescSortQueryString = "?sort=1";
                NameValueCollection qsDummy = HttpUtility.ParseQueryString(dateDescSortQueryString);

                // act
                IQueryBuilder<Product> productQueryBuilder = scope.Resolve<IQueryBuilder<Product>>();
                var products = await productQueryBuilder.Build(qsDummy);
                var result = products.Select(p => p.CreationDate).ToList();

                // assert
                Assert.True(result.Zip(result.Skip(1), (a, b) => new { a, b }).All(p => p.a.Date > p.b.Date));
            }
        }

        [Fact]
        public async void Build_PriceAsc2SortQueryString_ShouldReturnAllProductsWithTheSortOrder()
        {
            // 2.1. resolve dependency
            using (var container = _builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // resolve context
                CatalogApiDbContext context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                context = await SetupInitialDB(context);

                // 3. qs dummy
                string priceAscSortQueryString = "?sort=2";
                NameValueCollection qsDummy = HttpUtility.ParseQueryString(priceAscSortQueryString);

                // act
                IQueryBuilder<Product> productQueryBuilder = scope.Resolve<IQueryBuilder<Product>>();
                var products = await productQueryBuilder.Build(qsDummy);
                var result = products.Select(p => p.Price).ToList();

                // assert
                Assert.True(result.Zip(result.Skip(1), (a, b) => new { a, b }).All(p => p.a < p.b));
            }
        }

        [Fact]
        public async void Build_PriceDesc3SortQueryString_ShouldReturnAllProductsWithTheSortOrder()
        {
            // 2.1. resolve dependency
            using (var container = _builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // resolve context
                CatalogApiDbContext context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                context = await SetupInitialDB(context);

                // 3. qs dummy
                string priceDescSortQueryString = "?sort=3";
                NameValueCollection qsDummy = HttpUtility.ParseQueryString(priceDescSortQueryString);

                // act
                IQueryBuilder<Product> productQueryBuilder = scope.Resolve<IQueryBuilder<Product>>();
                var products = await productQueryBuilder.Build(qsDummy);
                var result = products.Select(p => p.Price).ToList();

                // assert
                Assert.True(result.Zip(result.Skip(1), (a, b) => new { a, b }).All(p => p.a > p.b));
            }
        }

        [Fact]
        public async void Build_NameAsc4SortQueryString_ShouldReturnAllProductsWithTheSortOrder()
        {
            // 2.1. resolve dependency
            using (var container = _builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // resolve context
                CatalogApiDbContext context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                context = await SetupInitialDB(context);

                // 3. qs dummy
                string nameAscSortQueryString = "?sort=4";
                NameValueCollection qsDummy = HttpUtility.ParseQueryString(nameAscSortQueryString);

                // act
                IQueryBuilder<Product> productQueryBuilder = scope.Resolve<IQueryBuilder<Product>>();
                var products = await productQueryBuilder.Build(qsDummy);
                var result = products.Select(p => p.Name).ToList();

                _output.WriteLine(JsonConvert.SerializeObject(result));

                // assert
                Assert.True(result.Zip(result.Skip(1), (a, b) => new { a, b }).All(p => p.a.CompareTo(p.b) < 0));
            }
        }

        [Fact]
        public async void Build_NameDesc5SortQueryString_ShouldReturnAllProductsWithTheSortOrder()
        {
            // 2.1. resolve dependency
            using (var container = _builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // resolve context
                CatalogApiDbContext context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                context = await SetupInitialDB(context);

                // 3. qs dummy
                string nameDescSortQueryString = "?sort=5";
                NameValueCollection qsDummy = HttpUtility.ParseQueryString(nameDescSortQueryString);

                // act
                IQueryBuilder<Product> productQueryBuilder = scope.Resolve<IQueryBuilder<Product>>();
                var products = await productQueryBuilder.Build(qsDummy);
                var result = products.Select(p => p.Name).ToList();

                _output.WriteLine(JsonConvert.SerializeObject(result));

                // assert
                Assert.True(result.Zip(result.Skip(1), (a, b) => new { a, b }).All(p => p.a.CompareTo(p.b) > 0));
            }
        }

        [Fact]
        public async void Build_ReviewAsc6SortQueryString_ShouldReturnAllProductsWithTheSortOrder()
        {
            // 2.1. resolve dependency
            using (var container = _builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // resolve context
                CatalogApiDbContext context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                context = await SetupInitialDB(context);

                // 3. qs dummy
                string ReviewAscSortQueryString = "?sort=6";
                NameValueCollection qsDummy = HttpUtility.ParseQueryString(ReviewAscSortQueryString);

                // act
                IQueryBuilder<Product> productQueryBuilder = scope.Resolve<IQueryBuilder<Product>>();
                var products = await productQueryBuilder.Build(qsDummy);
                var result = products.Select(p => p.Reviews.Count).ToList();

                _output.WriteLine(JsonConvert.SerializeObject(result));

                // assert
                Assert.True(result.Zip(result.Skip(1), (a, b) => new { a, b }).All(p => p.a >= p.b));
            }
        }

        [Fact]
        public async void Build_ReviewDesc7SortQueryString_ShouldReturnAllProductsWithTheSortOrder()
        {
            // 2.1. resolve dependency
            using (var container = _builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // resolve context
                CatalogApiDbContext context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                context = await SetupInitialDB(context);

                // 3. qs dummy
                string ReviewDescSortQueryString = "?sort=7";
                NameValueCollection qsDummy = HttpUtility.ParseQueryString(ReviewDescSortQueryString);

                // act
                IQueryBuilder<Product> productQueryBuilder = scope.Resolve<IQueryBuilder<Product>>();
                var products = await productQueryBuilder.Build(qsDummy);
                var result = products.Select(p => p.Reviews.Count).ToList();

                _output.WriteLine(JsonConvert.SerializeObject(result));

                // assert
                Assert.True(result.Zip(result.Skip(1), (a, b) => new { a, b }).All(p => p.a <= p.b));
            }
        }

        [Fact]
        public async void Build_ReviewScoreAsc8SortQueryString_ShouldReturnAllProductsWithTheSortOrder()
        {
            // 2.1. resolve dependency
            using (var container = _builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // resolve context
                CatalogApiDbContext context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                context = await SetupInitialDB(context);

                // 3. qs dummy
                string ReviewScoreAscSortQueryString = "?sort=8";
                NameValueCollection qsDummy = HttpUtility.ParseQueryString(ReviewScoreAscSortQueryString);

                // act
                IQueryBuilder<Product> productQueryBuilder = scope.Resolve<IQueryBuilder<Product>>();
                var products = await productQueryBuilder.Build(qsDummy);
                var result = products.Select(p => p.Reviews.Average(r => (int?)r.Score) ?? 0).ToList();

                _output.WriteLine(JsonConvert.SerializeObject(result));

                // assert
                Assert.True(result.Zip(result.Skip(1), (a, b) => new { a, b }).All(p => p.a >= p.b));
            }
        }

        [Fact]
        public async void Build_ReviewScoreDesc9SortQueryString_ShouldReturnAllProductsWithTheSortOrder()
        {
            // 2.1. resolve dependency
            using (var container = _builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // resolve context
                CatalogApiDbContext context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                context = await SetupInitialDB(context);

                // 3. qs dummy
                string ReviewScoreDescSortQueryString = "?sort=9";
                NameValueCollection qsDummy = HttpUtility.ParseQueryString(ReviewScoreDescSortQueryString);

                // act
                IQueryBuilder<Product> productQueryBuilder = scope.Resolve<IQueryBuilder<Product>>();
                var products = await productQueryBuilder.Build(qsDummy);
                var result = products.Select(p => p.Reviews.Average(r => (int?)r.Score) ?? 0).ToList();

                _output.WriteLine(JsonConvert.SerializeObject(result));

                // assert
                Assert.True(result.Zip(result.Skip(1), (a, b) => new { a, b }).All(p => p.a <= p.b));
            }
        }
    }
}
