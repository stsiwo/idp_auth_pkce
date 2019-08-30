using Bogus;
using OrderingApi.Domain.CartAgg;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace OrderingApiUnitTest.TestData.Entity
{
    public static class CartProductFaker
    {
        public static Faker<CartProduct> GetCartProductFaker()
        {
            var productFaker = new Faker<CartProduct>()
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.Name, f => new ProductName(f.Commerce.ProductName()))
                .RuleFor(p => p.Description, f => new ProductDescription(f.Random.Words(40)))
                .RuleFor(p => p.MainImageUrl, f => new ProductMainImageUrl(f.Image.LoremFlickrUrl()))
                .RuleFor(p => p.Price, f => new ProductPrice(f.Random.Decimal(1m, 10000m)))
                .RuleFor(p => p.Stock, f => new ProductStock(5, 2));
                // assign CartId, Cart, Cart, and CartId at each Faker

            return productFaker;
        }

        public static IList<CartProduct> GetCartProductList(int amount)
        {

            var faker = GetCartProductFaker();

            CartProduct SeededCartProduct(int seed)
            {
                return faker.UseSeed(seed).Generate();
            }

            var products = Enumerable.Range(1,amount)
                .Select(SeededCartProduct)
                .ToList();

            return products;  
        }
        public static IList<CartProduct> GetRandomCartProductList(int max)
        {
            var faker = GetCartProductFaker();

            CartProduct SeededCartProduct(int seed)
            {
                return faker.UseSeed(seed).Generate();
            }

            Random rnd = new Random();
            int maxNumberOfInstances = rnd.Next(0, max);

            var reviews = Enumerable.Range(1, maxNumberOfInstances)
                .Select(SeededCartProduct)
                .ToList();

            return reviews;  
        }
    }
}
