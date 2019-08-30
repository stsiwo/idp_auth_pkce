using AutoMapper;
using OrderingApi.Domain.OrderAgg;
using OrderingApi.UI.Model;

namespace OrderingApi.Config.AutoMapper.UIModeling
{
    public class MapOrderAggregateToUIModelProfile : Profile
    {
        public MapOrderAggregateToUIModelProfile()
        {
            CreateMap<Order, OrderModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            // OrderProduct
            CreateMap<OrderProduct, OrderProductModel>();

            CreateMap<ProductName, ProductNameModel>();

            CreateMap<ProductPrice, PriceModel>();

            CreateMap<ProductStock, StockModel>();

        }
    }
}
