using AutoMapper;
using CatalogApi.Application.DTO;
using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Configs.AutoMapper
{
    public class EntityDataToDTOProfile : Profile
    {
        public EntityDataToDTOProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<SubCategory, SubCategoryDTO>();
            CreateMap<Review, ReviewDTO>();
            CreateMap<SubImage, SubImageDTO>();
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.ReviewList, opt => opt.MapFrom(src => src.Reviews))
                .ForMember(dest => dest.SubImageURLList, opt => opt.MapFrom(src => src.SubImages));

        }

    }
}
