using Autofac;
using CatalogApi.Infrastructure;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.QueryBuilder;
using CatalogApiIntegrationTest.Extensioins;
using CatalogApiIntegrationTest.FunctionalTests.Fixtures.PerTest;
using CatalogApiIntegrationTest.FunctionalTests.Infrastructure.QueryBuilder.Data;
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
        private async Task<CatalogApiDbContext> SetupInitialDBForAllQueryString(CatalogApiDbContext context)
        {
            // this make sure clearing up the data from previous test
            context.Database.EnsureDeleted();
            // recreate database for current test
            context.Database.EnsureCreated();
            // 1.2. seed test data
            var productsTestData = AllQueryStringProductTestData.GetProducts(); 
            
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

                _output.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

                // assert
                Assert.True(result.Zip(result.Skip(1), (a, b) => new { a, b }).All(p => DateTime.Compare(p.a.Date, p.b.Date) <= 0));
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
                Assert.True(result.Zip(result.Skip(1), (a, b) => new { a, b }).All(p => p.a.Date <= p.b.Date));
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
                Assert.True(result.Zip(result.Skip(1), (a, b) => new { a, b }).All(p => p.a.Date >= p.b.Date));
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
                Assert.NotEmpty(products);
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
                Assert.NotEmpty(products);
                Assert.True(result);
            }
        }

        [Fact]
        public async void Build_KeyWordQueryString_ShouldReturnAllProductsWhoseKeyWordMatchTheQueryString()
        {
            // 2.1. resolve dependency
            using (var container = _builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // resolve _context
                CatalogApiDbContext _context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                _context = await SetupInitialDB(_context);

                // 2.2. dummy keyword
                string keywordDummy = "Table";

                // 3. qs dummy
                string KeyWordQueryString = "?keyword=" + keywordDummy;
                NameValueCollection qsDummy = HttpUtility.ParseQueryString(KeyWordQueryString);

                // act
                IQueryBuilder<Product> productQueryBuilder = scope.Resolve<IQueryBuilder<Product>>();
                var products = await productQueryBuilder.Build(qsDummy);
                var result = products.All(p => p.Name.Contains(keywordDummy) || p.Description.Contains(keywordDummy)); 
                _output.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));

                // assert
                Assert.NotEmpty(products);
                Assert.True(result);
            }
        }

        [Fact]
        public async void Build_ReviewScoreQueryString_ShouldReturnAllProductsWhoseReviewScoreMatchTheQueryString()
        {
            // 2.1. resolve dependency
            using (var container = _builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // resolve _context
                CatalogApiDbContext _context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                _context = await SetupInitialDB(_context);

                // 2.2. dummy reviewScore
                int reviewScoreDummy = 4;

                // 3. qs dummy
                string ReviewScoreQueryString = "?reviewscore=" + reviewScoreDummy;
                NameValueCollection qsDummy = HttpUtility.ParseQueryString(ReviewScoreQueryString);

                // act
                IQueryBuilder<Product> productQueryBuilder = scope.Resolve<IQueryBuilder<Product>>();
                var products = await productQueryBuilder.Build(qsDummy);
                var averageList = products.Select(p => p.Reviews.DefaultIfEmpty().Average(r => (int)r.Score));
                var result = products.All(p => Math.Round(p.Reviews.DefaultIfEmpty().Average(r => (int)r.Score)) == reviewScoreDummy); 
                //_output.WriteLine(JsonConvert.SerializeObject(averageList));
                //_output.WriteLine(JsonConvert.SerializeObject(_context.Products, Formatting.Indented));

                // assert
                Assert.NotEmpty(products);
                Assert.True(result);
            }
        }

        [Fact]
        public async void Build_MaxPriceQueryString_ShouldReturnAllProductsWhoseMaxPriceMatchTheQueryString()
        {
            // 2.1. resolve dependency
            using (var container = _builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // resolve _context
                CatalogApiDbContext _context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                _context = await SetupInitialDB(_context);

                // 2.2. dummy maxprice
                decimal maxpriceDummy = 30000m;

                // 3. qs dummy
                string MaxPriceQueryString = "?maxprice=" + maxpriceDummy;
                NameValueCollection qsDummy = HttpUtility.ParseQueryString(MaxPriceQueryString);

                // act
                IQueryBuilder<Product> productQueryBuilder = scope.Resolve<IQueryBuilder<Product>>();
                var products = await productQueryBuilder.Build(qsDummy);
                //var averageList = products.Select(p => p.Reviews.DefaultIfEmpty().Average(r => (int)r.Score));
                var result = products.All(p => p.Price < maxpriceDummy); 
                //_output.WriteLine(JsonConvert.SerializeObject(averageList));

                // assert
                Assert.NotEmpty(products);
                Assert.True(result);
            }
        }

        [Fact]
        public async void Build_MinPriceQueryString_ShouldReturnAllProductsWhoseMinPriceMatchTheQueryString()
        {
            // 2.1. resolve dependency
            using (var container = _builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // resolve _context
                CatalogApiDbContext _context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                _context = await SetupInitialDB(_context);

                // 2.2. dummy minprice
                decimal minpriceDummy = 30000m;

                // 3. qs dummy
                string MinPriceQueryString = "?minprice=" + minpriceDummy;
                NameValueCollection qsDummy = HttpUtility.ParseQueryString(MinPriceQueryString);

                // act
                IQueryBuilder<Product> productQueryBuilder = scope.Resolve<IQueryBuilder<Product>>();
                var products = await productQueryBuilder.Build(qsDummy);
                //var averageList = products.Select(p => p.Reviews.DefaultIfEmpty().Average(r => (int)r.Score));
                var result = products.All(p => p.Price > minpriceDummy); 
                _output.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));

                // assert
                Assert.NotEmpty(products);
                Assert.True(result);
            }
        }

        [Fact]
        public async void Build_AllQueryString_ShouldReturnAllProductsWhoseAllMatchTheQueryString()
        {
            // 2.1. resolve dependency
            using (var container = _builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // resolve _context
                CatalogApiDbContext _context = scope.Resolve<CatalogApiDbContext>();
                // seed initial data in inmemory
                _context = await SetupInitialDBForAllQueryString(_context);

                // 2.2. dummy all
                int categoryIdDummy = 0;
                int subCategoryIdDummy = 0;
                string keywordDummy = "collaborative";
                int reviewscoreDummy = 5;
                decimal maxpriceDummy = 70000m;
                decimal minpriceDummy = 60000m;

                // 3. qs dummy
                string AllQueryString = "?category=" + categoryIdDummy
                    + "&subcategory=" + subCategoryIdDummy
                    + "&keyword=" + keywordDummy
                    + "&reviewscore=" + reviewscoreDummy
                    + "&maxprice=" + maxpriceDummy
                    + "&minprice=" + minpriceDummy;

                NameValueCollection qsDummy = HttpUtility.ParseQueryString(AllQueryString);

                // act
                IQueryBuilder<Product> productQueryBuilder = scope.Resolve<IQueryBuilder<Product>>();
                var products = await productQueryBuilder.Build(qsDummy);
                //var averageList = products.Select(p => p.Reviews.DefaultIfEmpty().Average(r => (int)r.Score));
                var result = products.All(p => 
                {
                    return ((int)p.SubCategory.CategoryId == categoryIdDummy
                        && (int)p.SubCategory.Id == subCategoryIdDummy 
                        && p.Name.Contains(keywordDummy) || p.Description.Contains(keywordDummy)
                        && Math.Round(p.Reviews.DefaultIfEmpty().Average(r => (int)r.Score)) == reviewscoreDummy
                        && p.Price < maxpriceDummy
                        && p.Price > minpriceDummy);
                });
                _output.WriteLine(JsonConvert.SerializeObject(_context.Products, Formatting.Indented));

                // assert
                Assert.NotEmpty(products);
                Assert.True(result);
            }
        }
    }
}
