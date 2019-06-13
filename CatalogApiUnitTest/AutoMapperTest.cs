using AutoMapper;
using CatalogApi.Application.DTO;
using CatalogApi.Infrastructure.DataEntity;
using System;
using Xunit;

namespace CatalogApiUnitTest
{
    public class AutoMapperTest
    {
        [Fact]
        public void EntityDataToDTO()
        {
            var config = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<Category, CategoryDTO>();
                cfg.CreateMap<SubCategory, SubCategoryDTO>();
                cfg.CreateMap<Review, ReviewDTO>();
                cfg.CreateMap<SubImage, SubImageDTO>();
                cfg.CreateMap<Product, ProductDTO>()
                    .ForMember(dest => dest.ReviewList, opt => opt.MapFrom(src => src.Reviews))
                    .ForMember(dest => dest.SubImageURLList, opt => opt.MapFrom(src => src.SubImages));
            });

            var mapper = config.CreateMapper();

            mapper.ConfigurationProvider.AssertConfigurationIsValid();

        }
    }
}
