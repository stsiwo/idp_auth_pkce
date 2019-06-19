using CatalogApi;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApiIntegrationTest.Configs;
using CatalogApiIntegrationTest.TestData;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

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

        [Theory]
        [InlineData("/api/products")]
        public async Task GET_Endpoints_ReturnAllProductsWithoutStringQuery(string url)
        {
            // Arrange
            var client = _factory.CreateClient();
            // default sort is CreationDate (Asc)
            //var expectedResult = JsonConvert.SerializeObject(ProductsGETEndpointTestData.GetProducts().OrderBy(p => p.CreationDate), Formatting.Indented);
            var expectedResult = ProductsGETEndpointTestData.GetProducts().Count;

            // Act
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            // convert json to JObject
            JArray bodyJObject = JArray.Parse(body); 
            var result = bodyJObject.Count;

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
           
            Assert.Equal(expectedResult, result);

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

            Assert.Equal(expectedResult, result);

        }

        [Theory]
        [InlineData("/api/products?category=1")]
        public async Task GET_RequestWithCategoryQueryString_ShouldReturnAllProdcutWhoseCategoryMatchesWithQueryString(string url)
        {
            // Arrange
            var client = _factory.CreateClient();
            // default sort is CreationDate (Asc)
            var expectedResult = ProductsGETEndpointTestData.GetProducts().OrderBy(p => p.CreationDate).Where(p => p.SubCategory.CategoryId == (CategoryConstants)1).Select(p => p.SubCategory.CategoryId.ToString()).ToList();

            // Act
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            // convert json to JObject
            JArray bodyJArray = JArray.Parse(body);
            var result = bodyJArray.Select(o => (string)o["creationDate"]).ToList();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal(expectedResult, result);

        }
    }
}
