﻿using CatalogApi;
using CatalogApiIntegrationTest.Configs;
using Microsoft.AspNetCore.Mvc.Testing;
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
        private readonly ProductsControllerWebApplicationFactory<Startup> _factory;
        public GetEndpoint(ProductsControllerWebApplicationFactory<Startup> factory)  
        {
            _factory = factory;
        }

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
        public async Task GET_Endpoints_ReturnNotContentStatusCode(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();

            // Assert
            //response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("", body);

        }
    }
}
