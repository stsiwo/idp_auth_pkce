using Bogus;
using OrderingApi.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace OrderingApiUnitTest.TestData.Entity
{
    public static class OrderProductFaker
    {
        public static Faker<OrderProduct> GetOrderProductFaker()
        {
            var productFaker = new Faker<OrderProduct>()
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.Name, f => new ProductName(f.Commerce.ProductName()))
                .RuleFor(p => p.Price, f => new ProductPrice(f.Random.Decimal(1m, 10000m)))
                .RuleFor(p => p.Stock, f => new ProductStock(5, 2));
                // assign OrderId, Order, Cart, and CartId at each Faker

            return productFaker;
        }

        public static IList<OrderProduct> GetOrderProductList(int amount)
        {

            var faker = GetOrderProductFaker();

            OrderProduct SeededOrderProduct(int seed)
            {
                return faker.UseSeed(seed).Generate();
            }

            var products = Enumerable.Range(1,amount)
                .Select(SeededOrderProduct)
                .ToList();

            return products;  
        }
        public static IList<OrderProduct> GetRandomOrderProductList(int max)
        {
            var faker = GetOrderProductFaker();

            OrderProduct SeededOrderProduct(int seed)
            {
                return faker.UseSeed(seed).Generate();
            }

            Random rnd = new Random();
            int maxNumberOfInstances = rnd.Next(0, max);

            var reviews = Enumerable.Range(1, maxNumberOfInstances)
                .Select(SeededOrderProduct)
                .ToList();

            return reviews;  
        }
    }
}
