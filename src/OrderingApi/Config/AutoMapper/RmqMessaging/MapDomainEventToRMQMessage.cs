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

namespace OrderingApi.Config.AutoMapper.RmqMessaging
{
    public class MapDomainEventToRmqPublishMessage : Profile
    {
        public MapDomainEventToRmqPublishMessage()
        {
            var camelCaseSerializer = new JsonSerializer()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            CreateMap<IDomainEvent, RmqPublishMessage>()
                .IncludeAllDerived()
                // this is message id (not event id)
                .ForMember(dest => dest.MessageId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DomainEventType, opt => opt.MapFrom(src => src.DomainEventType))
                // get current project name as sender
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => System.Reflection.Assembly.GetEntryAssembly().GetName().Name))
                .ForMember(dest => dest.OccurredOn, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => JObject.FromObject(src, camelCaseSerializer)));

            // #EVENT
            CreateMap<CreatedCartDomainEvent, RmqPublishMessage>();
        }
    }
}
