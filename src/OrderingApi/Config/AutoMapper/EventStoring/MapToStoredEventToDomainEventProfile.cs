using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Config.AutoMapper.EventStoring
{
    public class MapStoredEventToDomainEventProfile : Profile
    {
        public MapStoredEventToDomainEventProfile()
        {
            CreateMap<StoredEvent, DomainEventBase>()
                .ForMember(dest => dest.DomainEventId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DomainEventName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DomainEventType, opt => opt.MapFrom(src => src.DomainEventType))
                .ForMember(dest => dest.OccurredOn, opt => opt.MapFrom(src => src.OccurredOn));

            /**
             * IMPORTANT NOTE: 
             *   interface can't be object so don't define "ForMember" at Interface (destination) use Base class instead.
             *   e.g., CreateMap<StoredEvent, IDomainEvent>().ForMember(...) ==> not gonna work!!!! don't do it
             *   e.g., CreateMap<StoredEvent, DomainEventBase>().ForMember(...) ==> OK!!!!! 
             **/


            // add new item when new event 
            // #EVENT
        }
    }
}
