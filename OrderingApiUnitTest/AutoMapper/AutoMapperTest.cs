using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Config.AutoMapper.EventStoring;
using OrderingApi.Config.AutoMapper.EventStoring.DomainEventImplMapping;
using OrderingApi.Domain.Base;
using System;
using Xunit;
using Xunit.Abstractions;

namespace OrderingApiUnitTest.DI
{
    public class AutoMapperTest
    {
        private readonly ITestOutputHelper _output;

        public AutoMapperTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Mapper_IDomainEventToStoredEvent_Test()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MapDomainEventToStoredEventProfile>();
                cfg.AddProfile<MapStoredEventToDomainEventProfile>();
                cfg.AddProfile<CartCreatedDomainEventProfile>();
            });

            IMapper mapper = config.CreateMapper();


            CartCreatedDomainEvent cartCreatedDomainEvent = new CartCreatedDomainEvent("test-id");

            var target = mapper.Map<StoredEvent>(cartCreatedDomainEvent);

            _output.WriteLine(JsonConvert.SerializeObject(target, Formatting.Indented));

            Assert.True(false);
        }

        [Fact]
        public void Mapper_StoredEventToDomainEvent_Test()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MapDomainEventToStoredEventProfile>();
                cfg.AddProfile<MapStoredEventToDomainEventProfile>();
                cfg.AddProfile<CartCreatedDomainEventProfile>();
            });

            IMapper mapper = config.CreateMapper();


            IDomainEvent cartCreatedDomainEvent = new CartCreatedDomainEvent("test-id");

            StoredEvent storedEvent = new StoredEvent()
            {
                Id = Guid.NewGuid(),
                Name = "CartCreatedDomainEvent", 
                DomainEventType = cartCreatedDomainEvent.DomainEventType,
                OccurredOn = cartCreatedDomainEvent.OccurredOn,
                Payload = JsonConvert.SerializeObject(cartCreatedDomainEvent, Formatting.None, new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                })
            };

            _output.WriteLine(JsonConvert.SerializeObject(storedEvent, Formatting.Indented));

            var target = Convert.ChangeType(mapper.Map(storedEvent, storedEvent.GetType(), Type.GetType(storedEvent.Name)), Type.GetType(storedEvent.Name));

            _output.WriteLine(JsonConvert.SerializeObject(target, Formatting.Indented));

            //Assert.True(target.DomainEventName == "test");
            Assert.Equal(typeof(CartCreatedDomainEvent), target.GetType());
        }
    }
}
