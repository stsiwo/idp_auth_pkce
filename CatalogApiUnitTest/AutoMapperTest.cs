using AutoMapper;
using CatalogApi.Application.DTO;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApiUnitTest.TestData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace CatalogApiUnitTest
{
    public class AutoMapperTest
    {
        private readonly ITestOutputHelper _output;

        public AutoMapperTest(ITestOutputHelper output)
        {
            _output = output;
        }

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

        [Fact]
        public void EntityDataToDTO_WithTestProductData()
        {
            // arrange
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

            var products = AllQueryStringProductTestData.GetProducts();
            _output.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));

            // act
            var mapper = config.CreateMapper();
            IList<ProductDTO> productDTOList = mapper.Map<IList<Product>, IList<ProductDTO>>(products);
            _output.WriteLine(JsonConvert.SerializeObject(productDTOList, Formatting.Indented));



            Assert.True(false);

        }
    }
}
