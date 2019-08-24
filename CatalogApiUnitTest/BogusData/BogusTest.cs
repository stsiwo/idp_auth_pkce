using Bogus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace CatalogApiUnitTest.BogusData
{
    public class BogusTest
    {
        private readonly ITestOutputHelper _output;

        public BogusTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact] 
        public void Bogus_Determinism_ShouldKeepTheDataConsistent()
        {
            // arrange
            // act
            var result = JsonConvert.SerializeObject(GetOrders());
            var expectedResult = JsonConvert.SerializeObject(GetOrders());

            _output.WriteLine(result);
            _output.WriteLine(expectedResult);

            // assert
            // when comparing two object, xUnit Assert.Equal does reference equality
            Assert.Equal(result, expectedResult);

        }

        private IList<Order> GetOrders()
        {
            var orderIds = 0;
            var orderFaker = new Faker<Order>()
                .RuleFor(o => o.OrderId, f => orderIds++)
                .RuleFor(o => o.Item, f => f.Commerce.Product())
                .RuleFor(o => o.Quantity, f => f.Random.Number(1, 5))
                .RuleFor(o => o.Description, f => f.Commerce.ProductAdjective()); //New Rule

            Order MakeOrder(int seed)
            {
                return orderFaker.UseSeed(seed).Generate();
            }

            var orders = Enumerable.Range(1, 1)
               .Select(MakeOrder)
               .ToList();

            return orders;

        }

        class Order
        {
            public int OrderId { get; set; }
            public string Item { get; set; }
            public int Quantity { get; set; }
            public string Description { get; set; }
        }
    }
}
