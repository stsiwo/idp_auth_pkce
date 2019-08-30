using AutoMapper;
using OrderingApi.Domain.CartAgg;
using OrderingApi.Domain.UserAgg;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Config.AutoMapper.UIModeling
{
    public class MapCartAggregateToUIModelProfile : Profile
    {
        public MapCartAggregateToUIModelProfile()
        {
            CreateMap<Cart, CartModel>();

            CreateMap<CartProduct, CartProductModel>();

            // CartProduct
            CreateMap<CartProduct, CartProductModel>()
                .ForMember(m => m.MainImageUrl, opt => opt.MapFrom(src => src.MainImageUrl.Url));

            CreateMap<ProductName, ProductNameModel>();

            CreateMap<ProductDescription, ProductDescriptionModel>();

            CreateMap<ProductPrice, PriceModel>();

            CreateMap<ProductStock, StockModel>();

        }
    }
}
