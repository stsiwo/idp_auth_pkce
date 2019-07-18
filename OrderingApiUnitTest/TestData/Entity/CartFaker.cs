using Bogus;
using OrderingApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderingApiUnitTest.TestData.Entity
{
    public static class CartFaker
    {
        public static Faker<Cart> GetCartFaker()
        {
            var cartFaker = new Faker<Cart>()
                .RuleFor(c => c.Id, f => Guid.NewGuid().ToString())
                .RuleFor(c => c.Products, f => ProductFaker.GetRandomProductList(20))
                .FinishWith((f, c) =>
                {
                    foreach (var product in c.Products)
                    {
                        product.CartId = c.Id;
                        // product.Cart = c; // avoid this circular dependency
                    }

                });
            // assign UserId and User at UserFaker

            return cartFaker;
        }

        public static IList<Cart> GetCartList(int amount)
        {

            var faker = GetCartFaker();

            Cart SeededCart(int seed)
            {
                return faker.UseSeed(seed).Generate();
            }

            var carts = Enumerable.Range(1,amount)
                .Select(SeededCart)
                .ToList();

            return carts;  
        }
        public static IList<Cart> GetRandomCartList(int max)
        {
            var faker = GetCartFaker();

            Cart SeededCart(int seed)
            {
                return faker.UseSeed(seed).Generate();
            }

            Random rnd = new Random();
            int maxNumberOfInstances = rnd.Next(0, max);

            var carts = Enumerable.Range(1, maxNumberOfInstances)
                .Select(SeededCart)
                .ToList();

            return carts;  
        }
    }
}
