using Autofac;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Core;
using CatalogApi.Infrastructure.Specification.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.DI
{
    public class ProductSpecificationFactoryModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            /**
             * mixing concept of Keyed Search Lookup (Keyed) and Parameterized Instantiation (Func<P, T>) in Autofac
             * 
             *  client => specificationFactory => each Specification
             **/
            // product specification
            builder.RegisterType<SpecificationFactory<IncludeCategorySpecification>>()
               .Keyed<ISpecificationFactory<ISpecification<Product>>>(QueryStringConstants.Category)
               .InstancePerLifetimeScope();

            builder.RegisterType<SpecificationFactory<IncludeKeyWordSpecification>>()
               .Keyed<ISpecificationFactory<ISpecification<Product>>>(QueryStringConstants.KeyWord)
               .InstancePerLifetimeScope();

            builder.RegisterType<SpecificationFactory<IncludeSubCategorySpecification>>()
               .Keyed<ISpecificationFactory<ISpecification<Product>>>(QueryStringConstants.SubCategory)
               .InstancePerLifetimeScope();

            builder.RegisterType<SpecificationFactory<IncludeReviewScoreSpecification>>()
               .Keyed<ISpecificationFactory<ISpecification<Product>>>(QueryStringConstants.ReviewScore)
               .InstancePerLifetimeScope();

            builder.RegisterType<SpecificationFactory<PriceIsLessThanOrEqualSpecification>>()
               .Keyed<ISpecificationFactory<ISpecification<Product>>>(QueryStringConstants.MaxPrice)
               .InstancePerLifetimeScope();

            builder.RegisterType<SpecificationFactory<PriceIsMoreThanOrEqualSpecification>>()
               .Keyed<ISpecificationFactory<ISpecification<Product>>>(QueryStringConstants.MinPrice)
               .InstancePerLifetimeScope();

            builder.RegisterType<IncludeCategorySpecification>()
                .InstancePerLifetimeScope();

            builder.RegisterType<IncludeKeyWordSpecification>()
                .InstancePerLifetimeScope();

            builder.RegisterType<IncludeSubCategorySpecification>()
                .InstancePerLifetimeScope();

            builder.RegisterType<IncludeReviewScoreSpecification>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PriceIsLessThanOrEqualSpecification>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PriceIsMoreThanOrEqualSpecification>()
                .InstancePerLifetimeScope();

        }
    }
}
