using Autofac;
using AutoMapper;
using CatalogApi.Application.DTO;
using CatalogApi.Application.Repository;
using CatalogApi.Application.Service.Products;
using CatalogApi.Configs.AutoMapper;
using CatalogApi.Infrastructure;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.QueryBuilder;
using CatalogApi.Infrastructure.Repository;
using CatalogApi.Infrastructure.Specification.Builder;
using CatalogApi.UI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.DI
{
    public class ProductsControllerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Controller itself is already registered by ASP.NET so you don't need to do it.

            // GET Endpoint 
            // === ProductsController's dependencies
            builder.RegisterType<Util>().As<IUtil>().InstancePerRequest();
            builder.RegisterType<GetProductsService>().As<IGetProductsService>().InstancePerRequest();

            // === GetProductsService's dependencies
            builder.RegisterType<ProductRepository>().As<IRepository<Product, ProductDTO>>().InstancePerRequest();

            // === ProductRepository's dependencies
            //   1. AutoMapper
            var mapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile<DefaultProfile>();
            });
            var mapper = mapperConfig.CreateMapper(); 
            builder.RegisterInstance(mapper).As<IMapper>().InstancePerRequest();

            //   2. QueryBuilder
            builder.RegisterType<ProductQueryBuilder>().As<IQueryBuilder<Product>>().InstancePerRequest();

            // === ProductQueryBuilder's dependencies
            builder.RegisterType<CatalogApiDbContext>().InstancePerRequest();
            builder.RegisterType<ProductSpecificationBuilder>().As<ISpecificationBuilder<Product>>().InstancePerRequest();
        }
    }
}
