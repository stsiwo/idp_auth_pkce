using Autofac;
using CatalogApi.DI;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Builder;
using CatalogApi.Infrastructure.Specification.Core;
using CatalogApi.Infrastructure.Specification.Products;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using Xunit;
using Xunit.Abstractions;

namespace CatalogApiIntegrationTest.FunctionalTests.Infrastructure.Specification.Builder
{
    public class ProductSpecificationBuilderTest
    {
        private readonly ITestOutputHelper _output;

        public ProductSpecificationBuilderTest(ITestOutputHelper output)
        {
            _output = output;
        }

        /**
         * assuming query string is validated at UI layer so don't need to test unspecified query key
         **/
        [Fact]
        public void Build_SpecificationBuilder_ShouldTransformAllQueryStringToExpressionTree()
        {
            // arrange
            var builder = new ContainerBuilder();

            builder.RegisterModule<ProductSpecificationFactoryModule>();
            builder.RegisterModule<SpecificationModule>();
            builder.RegisterType<ProductSpecificationBuilder>().As<ISpecificationBuilder<Product>>().InstancePerLifetimeScope();
            builder.RegisterType<ProductSpecificationBuilderClient>().InstancePerLifetimeScope();

            // queryString Dummy
            NameValueCollection qsDummy = HttpUtility.ParseQueryString("?category=1&subcategory=3&keyword=test&reviewscore=3&maxprice=300&minprice=200");


            // expectedResult 
            ISpecification<Product> expectedSpecification = new BaseSpecification<Product>()
                .And(new IncludeCategorySpecification("1"))
                .And(new IncludeSubCategorySpecification("3"))
                .And(new IncludeKeyWordSpecification("test"))
                .And(new IncludeReviewScoreSpecification("3"))
                .And(new PriceIsLessThanOrEqualSpecification("300"))
                .And(new PriceIsMoreThanOrEqualSpecification("200"));

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                ProductSpecificationBuilderClient client = scope.Resolve<ProductSpecificationBuilderClient>();

                Expression<Func<Product, bool>> result = client.Build(qsDummy);

                // you can display content of expression tree. just use ToString()
                //Expression<Func<int, bool>> expression = n => n > 5 && n < 3;
                //_output.WriteLine(expression.ToString());
                _output.WriteLine(result.ToString());

                // assert
                Assert.Equal(expectedSpecification.ToExpression().ToString(), result.ToString());
            }

        } 

    }
}
