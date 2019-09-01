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
                cfg.AddProfile<CreatedCartDomainEventProfile>();
            });

            IMapper mapper = config.CreateMapper();


            CreatedCartDomainEvent CreatedCartDomainEvent = new CreatedCartDomainEvent("test-id");

            var target = mapper.Map<StoredEvent>(CreatedCartDomainEvent);

            _output.WriteLine(JsonConvert.SerializeObject(target, Formatting.Indented));

            Assert.True(false);
        }

        [Fact]
        public void Mapper_StoredEventToDomainEvent_Test()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MapDomainEventToStoredEventProfile>();
                cfg.AddProfile<MapStoredEventToDomainEventProfile>();
                cfg.AddProfile<CreatedCartDomainEventProfile>();
            });

            IMapper mapper = config.CreateMapper();


            IDomainEvent CreatedCartDomainEvent = new CreatedCartDomainEvent("test-id");

            StoredEvent storedEvent = new StoredEvent()
            {
                Id = Guid.NewGuid(),
                Name = "CreatedCartDomainEvent", 
                DomainEventType = CreatedCartDomainEvent.DomainEventType,
                OccurredOn = CreatedCartDomainEvent.OccurredOn,
                Payload = JsonConvert.SerializeObject(CreatedCartDomainEvent, Formatting.None, new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                })
            };

            _output.WriteLine(JsonConvert.SerializeObject(storedEvent, Formatting.Indented));

            var target = Convert.ChangeType(mapper.Map(storedEvent, storedEvent.GetType(), Type.GetType(storedEvent.Name)), Type.GetType(storedEvent.Name));

            _output.WriteLine(JsonConvert.SerializeObject(target, Formatting.Indented));

            //Assert.True(target.DomainEventName == "test");
            Assert.Equal(typeof(CreatedCartDomainEvent), target.GetType());
        }
    }
}
