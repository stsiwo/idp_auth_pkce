using AutoMapper;
using Newtonsoft.Json.Linq;
using OrderingApi.Application.DomainEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Config.AutoMapper.RmqMessaging
{
    public class MapJObjectToDomainEvent : Profile
    {
        public MapJObjectToDomainEvent()
        {
            CreateMap<JObject, IDomainEvent>()
                .IncludeAllDerived();

            CreateMap<JObject, DomainEventBase>()
                .IncludeAllDerived()
                .ForMember(dest => dest.DomainEventId, opt => opt.MapFrom(src => src.GetValue("domainEventId")))
                .ForMember(dest => dest.DomainEventType, opt => opt.MapFrom(src => src.GetValue("domainEventType")));

            // #EVENT
            CreateMap<JObject, CartCreatedDomainEvent>()
                .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.GetValue("cartId")));


        }

    }
}
