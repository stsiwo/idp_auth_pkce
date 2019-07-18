using Bogus;
using OrderingApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace OrderingApiUnitTest.TestData.Entity
{
    public static class ProductFaker
    {
        public static Faker<Product> GetProductFaker()
        {
            var productFaker = new Faker<Product>()
                .RuleFor(p => p.Id, f => Guid.NewGuid().ToString())
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Random.Words(f.Random.Number(1, 400)))
                .RuleFor(p => p.MainImageURL, f => f.Image.PicsumUrl())
                .RuleFor(p => p.Price, f => Decimal.Parse(f.Commerce.Price(100m, 100000m), NumberStyles.Currency))
                .RuleFor(p => p.Stock, f => f.Random.Number(10))
                .RuleFor(p => p.AvailableStock, (f, p) =>
                {
                    // AvailableStock must be less than Stock and more than or equal 0
                    int availableStock = p.Stock - f.Random.Number(2);
                    if (availableStock < 0) availableStock = 0;
                    return availableStock;
                });
                // assign OrderId, Order, Cart, and CartId at each Faker

            return productFaker;
        }

        public static IList<Product> GetProductList(int amount)
        {

            var faker = GetProductFaker();

            Product SeededProduct(int seed)
            {
                return faker.UseSeed(seed).Generate();
            }

            var products = Enumerable.Range(1,amount)
                .Select(SeededProduct)
                .ToList();

            return products;  
        }
        public static IList<Product> GetRandomProductList(int max)
        {
            var faker = GetProductFaker();

            Product SeededProduct(int seed)
            {
                return faker.UseSeed(seed).Generate();
            }

            Random rnd = new Random();
            int maxNumberOfInstances = rnd.Next(0, max);

            var reviews = Enumerable.Range(1, maxNumberOfInstances)
                .Select(SeededProduct)
                .ToList();

            return reviews;  
        }
    }
}
