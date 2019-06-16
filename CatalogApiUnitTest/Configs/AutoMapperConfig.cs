using AutoMapper;
using CatalogApi.Application.DTO;
using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogApiUnitTest.Configs
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration GetAutoMapperConfig()
        {
            return new MapperConfiguration(cfg =>
            {
               cfg.CreateMap<Category, CategoryDTO>();
               cfg.CreateMap<SubCategory, SubCategoryDTO>();
               cfg.CreateMap<Review, ReviewDTO>();
               cfg.CreateMap<SubImage, SubImageDTO>();
               cfg.CreateMap<Product, ProductDTO>()
                    .ForMember(dest => dest.ReviewList, opt => opt.MapFrom(src => src.Reviews))
                    .ForMember(dest => dest.SubImageURLList, opt => opt.MapFrom(src => src.SubImages));
            });
        }

    }
}
