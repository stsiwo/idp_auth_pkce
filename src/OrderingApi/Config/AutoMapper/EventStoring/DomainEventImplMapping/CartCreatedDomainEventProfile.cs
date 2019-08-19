using AutoMapper;
using Newtonsoft.Json.Linq;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Config.AutoMapper.EventStoring.DomainEventImplMapping
{
    public class CartCreatedDomainEventProfile : Profile
    {
        public CartCreatedDomainEventProfile()
        {
            
            CreateMap<StoredEvent, CartCreatedDomainEvent>()
                .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => JObject.Parse(src.Payload).GetValue("cartId")))
                .IncludeBase<StoredEvent, DomainEventBase>();
        }
    }
}
