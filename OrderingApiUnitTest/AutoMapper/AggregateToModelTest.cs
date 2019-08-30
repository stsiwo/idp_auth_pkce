using AutoMapper;
using Newtonsoft.Json;
using OrderingApi.Config.AutoMapper.UIModeling;
using OrderingApi.Domain.UserAgg;
using OrderingApi.UI.Model;
using OrderingApiUnitTest.TestData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace OrderingApiUnitTest.AutoMapper
{
    public class AggregateToModelTest
    {
        private readonly ITestOutputHelper _output;

        public AggregateToModelTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Mapper_CartAggregate_ShouldMappedToCartUIModel()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MapCartAggregateToUIModelProfile>();
                cfg.AddProfile<MapOrderAggregateToUIModelProfile>();
                cfg.AddProfile<MapUserAggregateToUIModelProfile>();
            });

            IMapper mapper = config.CreateMapper();

            var cart = CartFaker.GetCartList(1).FirstOrDefault();

            var cartModel = mapper.Map<CartModel>(cart);

//            _output.WriteLine(JsonConvert.SerializeObject(mapper.Map<CartModel>(cart), Formatting.Indented, new JsonSerializerSettings()
//            {
//                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
//            }));

            Assert.IsType<CartModel>(cartModel);
        }
        
        [Fact]
        public void Mapper_OrderAggregate_ShouldMappedToOrderUIModel()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MapCartAggregateToUIModelProfile>();
                cfg.AddProfile<MapOrderAggregateToUIModelProfile>();
                cfg.AddProfile<MapUserAggregateToUIModelProfile>();
            });

            IMapper mapper = config.CreateMapper();

            
            var order = OrderFaker.GetOrderList(1).FirstOrDefault();

            var orderModel = mapper.Map<OrderModel>(order);

            _output.WriteLine(JsonConvert.SerializeObject(orderModel, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            }));
            //Assert.True(false);

            Assert.IsType<OrderModel>(orderModel);
        }

        [Fact]
        public void Mapper_UserAggregate_ShouldMappedToUserUIModel()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MapCartAggregateToUIModelProfile>();
                cfg.AddProfile<MapOrderAggregateToUIModelProfile>();
                cfg.AddProfile<MapUserAggregateToUIModelProfile>();
            });

            IMapper mapper = config.CreateMapper();

            var user = UserFaker.GetUserList(1).FirstOrDefault();

            var userModel = mapper.Map<UserModel>(user);

            _output.WriteLine(JsonConvert.SerializeObject(userModel, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            }));

            Assert.IsType<UserModel>(userModel);
        }

        [Fact]
        public void Mapper_AddressAggregate_ShouldMappedToAddressUIModel()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Address, AddressModel>();
            });

            IMapper mapper = config.CreateMapper();

            var address = new Address(
                "street",
                "city",
                "state",
                "country",
                "postalCode"
                );

            var addressModel = mapper.Map<AddressModel>(address);

            _output.WriteLine(JsonConvert.SerializeObject(addressModel, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            }));

            Assert.IsType<AddressModel>(addressModel);
        }
    }
}
