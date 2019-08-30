using AutoMapper;
using OrderingApi.Domain.UserAgg;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Config.AutoMapper.UIModeling
{
    public class MapUserAggregateToUIModelProfile : Profile
    {
        public MapUserAggregateToUIModelProfile()
        {
            CreateMap<User, UserModel>()
                // need to add this otherwise, stuck unit test and could get result. I don't know what's going on
                .ForMember(dest => dest.HomeAddress, opt => opt.MapFrom(src => src.HomeAddress));

            CreateMap<Name, NameModel>();

            CreateMap<Address, AddressModel>();

//            CreateMap<Phone, ContactModel>();
        }
    }
}
