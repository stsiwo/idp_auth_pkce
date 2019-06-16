using AutoMapper;
using CatalogApi.Application.DTO;
using CatalogApi.Application.Repository;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.QueryBuilder;
using CatalogApi.Infrastructure.Repository;
using CatalogApiUnitTest.Configs;
using CatalogApiUnitTest.TestData;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CatalogApiUnitTest.Infrastructure.Repository
{
    public class ProductRepositoryTest
    {
        [Fact]
        public async void GetList_ReturnAllProducts()
        {
            // arrange 
            // 1. query string dummy
            IDictionary<string, string> queryStringDummy = null;
            // 2. test data stub
            IList<Product> productsStub = ProductsTestData.GetProducts();
            // 3. queryBuilder stub
            var queryBuilderStub = new Mock<IQueryBuilder<Product>>();
            queryBuilderStub.Setup(qb => qb.Build(queryStringDummy)).Returns(Task.FromResult(productsStub));
            // 4. IMapper (use real one)
            IMapper mapper = new Mapper(AutoMapperConfig.GetAutoMapperConfig());
            // 5. ILogger 
            var loggerStub = new Mock<ILogger<ProductRepository>>();


            // act 
            IRepository<Product, ProductDTO> repository = new ProductRepository(mapper, queryBuilderStub.Object, loggerStub.Object);
            var result = await repository.GetList(queryStringDummy);

            // assert
            Assert.Equal(50, result.Count());


        }
    }
}
