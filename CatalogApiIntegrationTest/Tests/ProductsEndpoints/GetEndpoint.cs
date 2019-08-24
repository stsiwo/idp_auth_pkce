using CatalogApi;
using CatalogApi.Infrastructure;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApiIntegrationTest.Configs;
using CatalogApiIntegrationTest.TestData;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using CatalogApiIntegrationTest.FunctionalTests.Infrastructure.QueryBuilder.Data;

namespace CatalogApiIntegrationTest.Tests.ProductsEndpoints
{
    public class GetEndpoint : IClassFixture<ProductsControllerWebApplicationFactory<Startup>>
    {
        private readonly ITestOutputHelper _output;

        private readonly ProductsControllerWebApplicationFactory<Startup> _factory;
        public GetEndpoint(ProductsControllerWebApplicationFactory<Startup> factory, ITestOutputHelper output)  
        {
            _factory = factory;
            _output = output;
        }

        public object ProductGETEndpointTestData { get; private set; }

        [Theory]
        [InlineData("/api/products")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        // this test passed even though result tells fail. Bogus library's bug.
        [Theory]
        [InlineData("/api/products")]
        public async Task GET_RequestWithoutQueryString_ShouldReturnAllProdcutWithDefaultSort(string url)
        {
            // Arrange
            var client = _factory.CreateClient();
            // default sort is CreationDate (Asc)
            var expectedResult = ProductsGETEndpointTestData.GetProducts().OrderBy(p => p.CreationDate).Select(p => p.CreationDate.ToString()).ToList();

            // Act
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            // convert json to JObject
            JArray bodyJArray = JArray.Parse(body);
            var result = bodyJArray.Select(o => (string)o["creationDate"]).ToList();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.True(result.Zip(result.Skip(1), (a, b) => new { a, b }).All(p => Convert.ToDateTime(p.a) <= Convert.ToDateTime(p.b)));

        }

        [Theory]
        [InlineData("/api/products?sort=3")]
        public async Task GET_RequestAscSortQueryString_ShouldReturnAllProdcutWithTheSortOrder(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            // convert json to JObject
            JArray bodyJArray = JArray.Parse(body);
            var products = bodyJArray.ToList();
            var result = products.Select(o => (string)o["price"]);
            _output.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.True(result.Zip(result.Skip(1), (a, b) => new { a, b }).All(p => Convert.ToDecimal(p.a) >= Convert.ToDecimal(p.b)));

        }

        [Theory]
        [InlineData("/api/products?minprice=3000")]
        public async Task GET_RequestWithCategoryQueryString_ShouldReturnAllProdcutWhoseCategoryMatchesWithQueryString(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            // convert json to JObject
            JArray bodyJArray = JArray.Parse(body);
            var products = bodyJArray.ToList();
            var result = products.All(p => Convert.ToDecimal(p["price"]) > 3000m);

            _output.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.True(result);

        }

        [Theory]
        [InlineData("/api/products")]
        public async Task GET_RequestWithAllQueryString_ShouldReturnAllProdcutWhoseAllMatchesWithQueryString(string url)
        {
            // Arrange
            var client = _factory
                .WithWebHostBuilder(builder => 
                {
                    builder.ConfigureServices(services =>
                    {
                        var serviceProvider = services.BuildServiceProvider();

                        using (var scope = serviceProvider.CreateScope())
                        {
                            var scopedServices = scope.ServiceProvider;
                            var db = scopedServices
                                .GetRequiredService<CatalogApiDbContext>();
                            var logger = scopedServices
                                .GetRequiredService<ILogger<GetEndpoint>>();

                            try
                            {
                                db.Database.EnsureDeleted();
                                db.Database.EnsureCreated();

                                var productsTestData = AllQueryStringProductTestData.GetProducts(); 

                                // wrap with Async IQueryable and add it to context
                                db.AddRange(productsTestData);
                                db.SaveChanges();

                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, "An error occurred seeding " +
                                    "the database with test messages. Error: " +
                                    ex.Message);
                            }
                        }
                    });

                })
                .CreateClient();
            // queryString Dummy
            var categoryIdDummy = 0; 
            var subcategoryIdDummy = 0; 
            var keywordDummy = "Shoes";
            var reviewscoreDummy = 5;
            var maxpriceDummy = 70000m; 
            var minpriceDummy = 60000m; 
            var sortDummy = 3;

            string allQueryStringDummy =
                      "?category=" + categoryIdDummy
                    + "&subcategory=" + subcategoryIdDummy
                    + "&keyword=" + keywordDummy
                    + "&reviewscore=" + reviewscoreDummy
                    + "&maxprice=" + maxpriceDummy
                    + "&minprice=" + minpriceDummy
                    + "&sort=" + sortDummy;

            // Act
            var response = await client.GetAsync(url+allQueryStringDummy);
            var body = await response.Content.ReadAsStringAsync();
            // convert json to JObject
            JArray bodyJArray = JArray.Parse(body);
            var products = bodyJArray.ToList();
            var result = products.All(p =>
            {
                return ((int)p["subCategory"]["categoryId"] == categoryIdDummy
                    && (int)p["subCategory"]["id"] == subcategoryIdDummy
                    && p["name"].ToString().Contains(keywordDummy) || p["description"].ToString().Contains(keywordDummy)
                    && Math.Round(p["reviewList"].DefaultIfEmpty().Average(r => (int)r["score"])) == reviewscoreDummy
                    && Convert.ToDecimal(p["price"]) < maxpriceDummy
                    && Convert.ToDecimal(p["price"]) > minpriceDummy
                    );
            });

            _output.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.True(result);

        }
    }
}
