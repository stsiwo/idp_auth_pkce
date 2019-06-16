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
            builder.RegisterType<SpecificationFactory<string, IncludeCategorySpecification>>()
               .Keyed<ISpecificationFactory<string, ISpecification<Product>>>(QueryConstants.Category)
               .InstancePerLifetimeScope();

            builder.RegisterType<SpecificationFactory<string, IncludeKeyWordSpecification>>()
               .Keyed<ISpecificationFactory<string, ISpecification<Product>>>(QueryConstants.KeyWord)
               .InstancePerLifetimeScope();

            builder.RegisterType<SpecificationFactory<string, IncludeSubCategorySpecification>>()
               .Keyed<ISpecificationFactory<string, ISpecification<Product>>>(QueryConstants.SubCategory)
               .InstancePerLifetimeScope();

            builder.RegisterType<SpecificationFactory<ScoreConstants, IncludeReviewScoreSpecification>>()
               .Keyed<ISpecificationFactory<ScoreConstants, ISpecification<Product>>>(QueryConstants.ReviewScore)
               .InstancePerLifetimeScope();

            builder.RegisterType<SpecificationFactory<decimal, PriceIsLessThanOrEqualSpecification>>()
               .Keyed<ISpecificationFactory<decimal, ISpecification<Product>>>(QueryConstants.MaxPrice)
               .InstancePerLifetimeScope();

            builder.RegisterType<SpecificationFactory<decimal, PriceIsMoreThanOrEqualSpecification>>()
               .Keyed<ISpecificationFactory<decimal, ISpecification<Product>>>(QueryConstants.MinPrice)
               .InstancePerLifetimeScope();

        }

    }
}
