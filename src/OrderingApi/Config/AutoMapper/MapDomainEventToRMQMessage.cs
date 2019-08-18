using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Infrastructure.RabbitMQ.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Config.AutoMapper
{
    public class MapDomainEventToRmqMessage : Profile
    {
        public MapDomainEventToRmqMessage()
        {
            var camelCaseSerializer = new JsonSerializer()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            CreateMap<IDomainEvent, RmqMessage>()
                .IncludeAllDerived()
                // this is message id (not event id)
                .ForMember(dest => dest.MessageId, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.DomainEventType, opt => opt.MapFrom(src => src.GetDomainEventType()))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => JObject.FromObject(src, camelCaseSerializer)));

            CreateMap<CartCreatedDomainEvent, RmqMessage>();
        }
    }
}
