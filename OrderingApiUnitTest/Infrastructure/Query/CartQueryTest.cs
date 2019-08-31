using AutoMapper;
using Moq;
using Newtonsoft.Json;
using NHibernate;
using NHibernate.Linq;
using OrderingApi;
using OrderingApi.Config.AutoMapper.UIModeling;
using OrderingApi.Domain.CartAgg;
using OrderingApi.Infrastructure.Query;
using OrderingApi.UI.Model;
using OrderingApiUnitTest.Infrastructure.DataBase;
using OrderingApiUnitTest.TestData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace OrderingApiUnitTest.Infrastructure.Query
{
    public class CartQueryTest : InMemoryDatabaseTest
    {
        private readonly ITestOutputHelper _output;
        public CartQueryTest(ITestOutputHelper output) : base(typeof(Program).Assembly)
        {
            _output = output;
        }

        [Fact]
        public async void GetCartsByIds_CartQuery_ShouldReturnListOfCartModel()
        {
            // arrange
            var num = 5;
            var cartListDummy = CartFaker.GetCartList(num);
            var cartIdsListDummy = cartListDummy.Select(c => c.Id).ToList();

            // save test data
            using (var tx = session.BeginTransaction())
            {
                foreach (var cart in cartListDummy)
                {
                    session.Save(cart);
                }
                tx.Commit();
            }

            session.Clear();

            var sut = new CartQuery(session);
            // act
            var result = await sut.GetCartsByIds(cartIdsListDummy);

            // assert
            Assert.Equal(num, result.Count);
        }

        [Fact]
        public async void ListContains_Test()
        {
            Guid id = Guid.NewGuid();
            IList<string> list = new List<string>()
            {
               id.ToString(),
               Guid.NewGuid().ToString(),
               Guid.NewGuid().ToString(),
               Guid.NewGuid().ToString()
            }; 

            Assert.True(list.Contains(id.ToString()));
        }
    }
}
