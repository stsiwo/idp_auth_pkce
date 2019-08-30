﻿using AutoMapper;
using Moq;
using Newtonsoft.Json;
using NHibernate;
using NHibernate.Linq;
using OrderingApi.Domain.CartAgg;
using OrderingApi.Infrastructure.Query;
using OrderingApi.UI.Model;
using OrderingApiUnitTest.TestData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace OrderingApiUnitTest.Infrastructure.Query
{
    public class CartQueryTest
    {
        private readonly ITestOutputHelper _output;
        public CartQueryTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async void GetCartsByIds_CartQuery_ShouldReturnListOfCartModel()
        {
            // arrange
            var num = 5;
            var cartListDummy = CartFaker.GetCartList(num).AsQueryable();
            var cartIdsListDummy = cartListDummy.Select(c => c.Id.ToString()).ToList();

            var QueryMock = new Mock<IQuery>();
            var ISessionMock = new Mock<ISession>();
            ISessionMock.Setup(sess => sess.Query<Cart>()).Returns(cartListDummy);

            var MapperMock = new Mock<IMapper>();
            MapperMock.Setup(mapper => mapper.Map<CartModel>(It.IsAny<IList<Cart>>())).Returns(new CartModel());

            var sut = new CartQuery(ISessionMock.Object, MapperMock.Object);
            // act
            var result = await sut.GetCartsByIds(cartIdsListDummy);

            // assert
            Assert.Equal(num, result.Count);
        }
    }
}
