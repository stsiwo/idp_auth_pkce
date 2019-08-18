using Bogus;
using Domain = OrderingApi.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderingApiUnitTest.TestData.Entity
{
//    public static class OrderFaker
//    {
//        public static Faker<Order> GetOrderFaker()
//        {
//            var orderFaker = new Faker<Order>()
//                .RuleFor(c => c.Id, f => Guid.NewGuid().ToString())
//                .RuleFor(c => c.Status, f => (Domain::OrderStatusConstants)f.Random.Number(Enum.GetNames(typeof(Domain::OrderStatusConstants)).Length - 1))
//                .RuleFor(c => c.Products, f => ProductFaker.GetRandomProductList(20))
//                .FinishWith((f, o) =>
//                {
//                    foreach (var product in o.Products)
//                    {
//                        product.OrderId = o.Id;
//                        // product.Cart = c; // avoid this circular dependency
//                    }
//                });
//                // assign UserId and User at UserFaker
//
//            return orderFaker;
//        }
//
//        public static IList<Order> GetOrderList(int amount)
//        {
//
//            var faker = GetOrderFaker();
//
//            Order SeededOrder(int seed)
//            {
//                return faker.UseSeed(seed).Generate();
//            }
//
//            var orders = Enumerable.Range(1,amount)
//                .Select(SeededOrder)
//                .ToList();
//
//            return orders;  
//        }
//        public static IList<Order> GetRandomOrderList(int max)
//        {
//            var faker = GetOrderFaker();
//
//            Order SeededOrder(int seed)
//            {
//                return faker.UseSeed(seed).Generate();
//            }
//
//            Random rnd = new Random();
//            int maxNumberOfInstances = rnd.Next(0, max);
//
//            var orders = Enumerable.Range(1, maxNumberOfInstances)
//                .Select(SeededOrder)
//                .ToList();
//
//            return orders;  
//        }
//    }
}
