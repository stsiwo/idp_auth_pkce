using CatalogApi;
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
    }
}
