using Autofac;
using CatalogApi.Application.DTO;
using CatalogApi.Application.Repository;
using CatalogApi.Application.Service.Products;
using CatalogApi.Infrastructure;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.QueryBuilder;
using CatalogApi.Infrastructure.Repository;
using CatalogApi.Infrastructure.Specification.Builder;
using CatalogApi.UI.Utils;

namespace CatalogApi.DI
{
    public class ProductsControllerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            /**
             * you don't need to use "InstancePerRequest" in ASP.NET Core. 
             * "InstancePerRequest" is for ASP.NET (previous version of ASP.NET Core)
             * Instead, you can use "InstancePerLifetimeScope" in ASP.NET Core to get the same effect as "InstancePerRequest"
             *  - this is because Autofac was in charge of setting up the per-request lifetime scope in ASP.NET. but with the introduction of Microsoft.ExtensionsDependencyInjection, 
             *    the creation of per-request and other child lifetime scopes is now part of the confirming container (IServiceProvider) provided by the framework
             **/

            // GET Endpoint 
            // === ProductsController's dependencies
            builder.RegisterType<Util>().As<IUtil>().InstancePerLifetimeScope();
            builder.RegisterType<GetProductsService>().As<IGetProductsService>().InstancePerLifetimeScope();

            // === GetProductsService's dependencies
            builder.RegisterType<ProductRepository>().As<IRepository<Product, ProductDTO>>().InstancePerLifetimeScope();

            // === ProductRepository's dependencies

            //   2. QueryBuilder
            builder.RegisterType<ProductQueryBuilder>().As<IQueryBuilder<Product>>().InstancePerLifetimeScope();

            // === ProductQueryBuilder's dependencies
            builder.RegisterType<CatalogApiDbContext>().InstancePerLifetimeScope();
            builder.RegisterType<ProductSpecificationBuilder>().As<ISpecificationBuilder<Product>>().InstancePerLifetimeScope();
        }
    }
}
