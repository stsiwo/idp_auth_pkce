using Autofac;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.QueryBuilder;
using CatalogApi.Infrastructure.QueryBuilder.OrderClauseStrategy;
using CatalogApi.Infrastructure.Specification.Core;
using CatalogApi.Infrastructure.Specification.Products;
using CatalogApi.Test;
using CatalogApiUnitTest.DI.TestComponents;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CatalogApiUnitTest
{
    public class DITest
    {
        public object OrderConstants { get; private set; }
        private readonly ITestOutputHelper _output;

        public DITest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void DI_Test()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Dep>().As<IDep>()
                   .InstancePerLifetimeScope();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                var dep1 = scope.Resolve<IDep>();
                var dep2 = scope.Resolve<IDep>();

                Assert.Equal(dep1, dep2);
            }

        }

        [Fact]
        public void DI_Container_ShouldResolveImplBasedOnRuntimeParameter()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<NameAscStrategy>()
               .Keyed<IOrderClauseStrategy>(SortConstants.NameAsc)
               .InstancePerDependency();

            builder.RegisterType<NameDescStrategy>()
               .Keyed<IOrderClauseStrategy>(SortConstants.NameDesc)
               .InstancePerDependency();

            builder.RegisterType<TestQueryBuilder>()
                .InstancePerDependency();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                TestQueryBuilder tQB = scope.Resolve<TestQueryBuilder>();
                Assert.Equal("CatalogApi.Infrastructure.QueryBuilder.OrderClauseStrategy.NameAscStrategy", tQB.Build(4));
                Assert.Equal("CatalogApi.Infrastructure.QueryBuilder.OrderClauseStrategy.NameDescStrategy", tQB.Build(5));

                
            }

        }

        [Fact]
        public void DI_Container_ShouldResolveComponentWithParamAtRuntime()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<NamedParam>().As<INamedParam>()
               .InstancePerDependency();


            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                INamedParam np = scope.Resolve<INamedParam>(new NamedParameter("name", "satoshi"));
                Assert.Equal("satoshi", np.GetName());
            }

        }

        [Fact]
        public void DI_Container_ShouldResolveDynamicallyWithoutRegistration()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DynamicComponent>().InstancePerDependency();
            builder.RegisterType<DynamicClient>().InstancePerDependency();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                DynamicClient np = scope.Resolve<DynamicClient>();
                Assert.Equal("satoshi", np.GetMessage());
            }

        }

        [Fact]
        public void DI_Container_ShouldResolveImplWithRuntimeParameter()
        {
            var builder = new ContainerBuilder();

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

            builder.RegisterType<SpecificationFactoryClient>().InstancePerDependency();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                SpecificationFactoryClient c = scope.Resolve<SpecificationFactoryClient>();

                c.SetSpecification(QueryStringConstants.Category, Convert.ToString((int)CategoryConstants.Category1));

                Assert.Equal("CatalogApi.Infrastructure.Specification.Products.IncludeCategorySpecification", c.GetSpecificationType());

                c.SetSpecification(QueryStringConstants.KeyWord, "some KeyWord");

                Assert.Equal("CatalogApi.Infrastructure.Specification.Products.IncludeKeyWordSpecification", c.GetSpecificationType());

                c.SetSpecification(QueryStringConstants.MaxPrice, "30000");

                Assert.Equal("CatalogApi.Infrastructure.Specification.Products.PriceIsLessThanOrEqualSpecification", c.GetSpecificationType());
            }
        }
    }
}
