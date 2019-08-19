using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Config.AutoMapper.EventStoring
{
    public class MapDomainEventToStoredEventProfile : Profile
    {
        public MapDomainEventToStoredEventProfile()
        {
            CreateMap<IDomainEvent, StoredEvent>()
                .IncludeAllDerived()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DomainEventId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DomainEventName))
                .ForMember(dest => dest.DomainEventType, opt => opt.MapFrom(src => src.DomainEventType))
                .ForMember(dest => dest.OccurredOn, opt => opt.MapFrom(src => src.OccurredOn))
                .ForMember(dest => dest.Payload, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src, Formatting.None, new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                })));

            CreateMap<DomainEventBase, StoredEvent>()
                .IncludeAllDerived();

            // add new item when new event
            // #EVENT
            CreateMap<CartCreatedDomainEvent, StoredEvent>();
        }
    }
}
