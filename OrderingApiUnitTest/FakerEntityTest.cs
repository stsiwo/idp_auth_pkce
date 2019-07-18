using Newtonsoft.Json;
using OrderingApi.Infrastructure.DataEntity;
using OrderingApiUnitTest.TestData.Entity;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace OrderingApiUnitTest
{
    public class FakerEntityTest
    {
        private readonly ITestOutputHelper _output;
        public FakerEntityTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ProductFaker_GetProductList_ShouldBeProperlyConfigured()
        {
            IList<Product> products = ProductFaker.GetProductList(10);

            _output.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));

            Assert.Equal(10, products.Count);

        }

        [Fact]
        public void ProductFaker_GetRandomProductList_ShouldBeProperlyConfigured()
        {
            IList<Product> products = ProductFaker.GetRandomProductList(10);

            _output.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));

            Assert.True(products.Count <= 10);

        }

        [Fact]
        public void CartFaker_GetCartList_ShouldBeProperlyConfigured()
        {
            IList<Cart> products = CartFaker.GetCartList(10);

            _output.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));

            //Assert.True(false);
            Assert.True(products.Count == 10);

        }

        [Fact]
        public void CartFaker_GetRandomCartList_ShouldBeProperlyConfigured()
        {
            IList<Cart> products = CartFaker.GetRandomCartList(10);

            _output.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));

            Assert.True(products.Count <= 10);

        }

        [Fact]
        public void OrderFaker_GetOrderList_ShouldBeProperlyConfigured()
        {
            IList<Order> products = OrderFaker.GetOrderList(10);

            _output.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));

//            Assert.True(false);
            Assert.True(products.Count == 10);

        }

        [Fact]
        public void OrderFaker_GetRandomOrderList_ShouldBeProperlyConfigured()
        {
            IList<Order> products = OrderFaker.GetRandomOrderList(10);

            _output.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));

            Assert.True(products.Count <= 10);

        }

        [Fact]
        public void UserFaker_GetUserList_ShouldBeProperlyConfigured()
        {
            IList<User> products = UserFaker.GetUserList(1);

            _output.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));

            //Assert.True(false);
            Assert.True(products.Count == 1);

        }

        [Fact]
        public void UserFaker_GetRandomUserList_ShouldBeProperlyConfigured()
        {
            IList<User> products = UserFaker.GetRandomUserList(1);

            _output.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));

            Assert.True(products.Count <= 1);

        }
    }
}
