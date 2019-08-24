using Autofac;
using CatalogApi.DI;
using CatalogApiIntegrationTest.FunctionalTests.Infrastructure.Specification.Builder;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Xunit;

namespace CatalogApiIntegrationTest.FunctionalTests.UI.Validators
{
    public class QueryStringValidatorTest
    {
        [Fact]
        public void Validate_AllQueryString_ShouldBeProperlyValidatedAndFiltered()
        {
            // arrange
            var builder = new ContainerBuilder();

            builder.RegisterModule<ProductQueryStringValidatorModule>();
            builder.RegisterType<QueryStringValidatorClient>().InstancePerLifetimeScope();


            // queryString Dummy
            var categoryIdDummy = 3; 
            var subcategoryIdDummy = 12; 
            var keywordDummy = "myTest";
            var reviewscoreDummy = 3;
            var maxpriceDummy = 3000m; 
            var minpriceDummy = 2000m; 
            var sortDummy = 3;

            string allQueryStringDummy =
                      "?category=" + categoryIdDummy
                    + "&subcategory=" + subcategoryIdDummy
                    + "&keyword=" + keywordDummy
                    + "&reviewscore=" + reviewscoreDummy
                    + "&maxprice=" + maxpriceDummy
                    + "&minprice=" + minpriceDummy
                    + "&sort=" + sortDummy;

            // expectedResult 
            var expectedResult = allQueryStringDummy;

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                QueryStringValidatorClient client = scope.Resolve<QueryStringValidatorClient>();
                var result = HttpUtility.ParseQueryString(client.GetValidatedQueryString(allQueryStringDummy));


                // assert
                Assert.Equal(categoryIdDummy.ToString(), result["category"]);
                Assert.Equal(subcategoryIdDummy.ToString(), result["subcategory"]);
                Assert.Equal(keywordDummy.ToString(), result["keyword"]);
                Assert.Equal(reviewscoreDummy.ToString(), result["reviewscore"]);
                Assert.Equal(maxpriceDummy.ToString(), result["maxprice"]);
                Assert.Equal(minpriceDummy.ToString(), result["minprice"]);
                Assert.Equal(sortDummy.ToString(), result["sort"]);
            }
        }

        [Fact]
        public void Validate_UnDefinedQueryString_ShouldBeRemoved()
        {
            // arrange
            var builder = new ContainerBuilder();

            builder.RegisterModule<ProductQueryStringValidatorModule>();
            builder.RegisterType<QueryStringValidatorClient>().InstancePerLifetimeScope();

            // queryString Dummy
            var categoryIdDummy = 3; 
            var subcategoryIdDummy = 12; 
            var keywordDummy = "myTest";
            var reviewscoreDummy = 3;
            var maxpriceDummy = 3000m; 
            var minpriceDummy = 2000m; 
            var sortDummy = 3;
            var unspecifiedDummy = 1234;

            string allQueryStringDummy =
                      "?category=" + categoryIdDummy
                    + "&subcategory=" + subcategoryIdDummy
                    + "&keyword=" + keywordDummy
                    + "&reviewscore=" + reviewscoreDummy
                    + "&maxprice=" + maxpriceDummy
                    + "&minprice=" + minpriceDummy
                    + "&sort=" + sortDummy
                    + "&unspecified=" + unspecifiedDummy;

            // expectedResult 
            var expectedResult = allQueryStringDummy;

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                QueryStringValidatorClient client = scope.Resolve<QueryStringValidatorClient>();
                var result = HttpUtility.ParseQueryString(client.GetValidatedQueryString(allQueryStringDummy));


                // assert
                Assert.Equal(categoryIdDummy.ToString(), result["category"]);
                Assert.Equal(subcategoryIdDummy.ToString(), result["subcategory"]);
                Assert.Equal(keywordDummy.ToString(), result["keyword"]);
                Assert.Equal(reviewscoreDummy.ToString(), result["reviewscore"]);
                Assert.Equal(maxpriceDummy.ToString(), result["maxprice"]);
                Assert.Equal(minpriceDummy.ToString(), result["minprice"]);
                Assert.Equal(sortDummy.ToString(), result["sort"]);
                Assert.Null(result["unspecificed"]);
            }
        }
    }
}
