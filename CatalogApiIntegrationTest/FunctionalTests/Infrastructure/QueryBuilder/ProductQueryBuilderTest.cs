using Autofac;
using CatalogApi.Infrastructure;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.QueryBuilder;
using CatalogApiIntegrationTest.Extensioins;
using CatalogApiIntegrationTest.FunctionalTests.Fixtures.PerTest;
using CatalogApiIntegrationTest.TestData;
using CatalogApiIntegrationTest.TestData.Entity;
using Newtonsoft.Json;
using System;
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
        private CatalogApiDbContext _context;

        public ProductQueryBuilderTest(ITestOutputHelper output)
        {
            _output = output;
            _builder = DISetup.GetAutofacContainerBuilder();
            _context = null; 
        }

        private async Task<CatalogApiDbContext> SetupInitialDB(CatalogApiDbContext context)
        {
            // this make sure clearing up the data from previous test
            context.Database.EnsureDeleted();
            // recreate database for current test
            context.Database.EnsureCreated();
            // 1.2. seed test data
            var productsTestData = ProductFaker.GetProductList(50); 
            
            // wrap with Async IQueryable and add it to context
            context.AddRange(productsTestData);
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
                _context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                _context = await SetupInitialDB(_context);
                // expectedResult
                var expectedResult = ProductFaker.GetProductList(50).Count; 

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
                // resolve _context
                _context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                _context = await SetupInitialDB(_context);

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
                // resolve _context
                _context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                _context = await SetupInitialDB(_context);

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
                // resolve _context
                _context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                _context = await SetupInitialDB(_context);

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
                // resolve _context
                _context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                _context = await SetupInitialDB(_context);

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
                // resolve _context
                CatalogApiDbContext _context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                _context = await SetupInitialDB(_context);

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
                // resolve _context
                CatalogApiDbContext _context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                _context = await SetupInitialDB(_context);

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
                // resolve _context
                CatalogApiDbContext _context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                _context = await SetupInitialDB(_context);

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
                // resolve _context
                CatalogApiDbContext _context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                _context = await SetupInitialDB(_context);

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
                // resolve _context
                CatalogApiDbContext _context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                _context = await SetupInitialDB(_context);

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
                // resolve _context
                CatalogApiDbContext _context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                _context = await SetupInitialDB(_context);

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
                // resolve _context
                CatalogApiDbContext _context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                _context = await SetupInitialDB(_context);

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

        [Fact]
        public async void Build_CategoryQueryString_ShouldReturnAllProductsWhoseCategoryMatchTheQueryString()
        {
            // 2.1. resolve dependency
            using (var container = _builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // resolve _context
                CatalogApiDbContext _context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                _context = await SetupInitialDB(_context);

                // 3. qs dummy
                string CategoryQueryString = "?category=0";
                NameValueCollection qsDummy = HttpUtility.ParseQueryString(CategoryQueryString);

                // act
                IQueryBuilder<Product> productQueryBuilder = scope.Resolve<IQueryBuilder<Product>>();
                var products = await productQueryBuilder.Build(qsDummy);
                var result = products.All(p => (int)p.SubCategory.CategoryId == 0);

                // assert
                Assert.True(result);
            }
        }

        [Fact]
        public async void Build_SubCategoryQueryString_ShouldReturnAllProductsWhoseSubCategoryMatchTheQueryString()
        {
            // 2.1. resolve dependency
            using (var container = _builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // resolve _context
                CatalogApiDbContext _context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                _context = await SetupInitialDB(_context);

                // 3. qs dummy
                string SubCategoryQueryString = "?subcategory=40";
                NameValueCollection qsDummy = HttpUtility.ParseQueryString(SubCategoryQueryString);

                // act
                IQueryBuilder<Product> productQueryBuilder = scope.Resolve<IQueryBuilder<Product>>();
                var products = await productQueryBuilder.Build(qsDummy);
                var result = products.All(p => (int)p.SubCategory.Id == 40);

                _output.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));

                // assert
                Assert.True(false);
            }
        }
    }
}
