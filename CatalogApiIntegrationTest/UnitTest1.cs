using CatalogApi.Infrastructure.DataEntity;
using CatalogApiIntegrationTest.TestData;
using Newtonsoft.Json.Linq;
using System;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace CatalogApiIntegrationTest
{
    public class UnitTest1 : TestBase
    {
        public UnitTest1(ITestOutputHelper output) : base(output)
        {

        }
        [Fact]
        public void Test1()
        {
            string category1String = CategoryConstants.Category1.ToString();

            Assert.Equal("Category1", category1String);

        }

        [Fact]
        public void ProductsGETEndpointTestData_ShouldReturnListOfProducts()
        {
            var productsJson = ProductsGETEndpointTestData.GetProducts();

            _output.WriteLine("products json {0}", JToken.Parse(productsJson));

            Assert.Equal("json", productsJson);
        }
    }
}
