using CatalogApi.UI.Validators.ProductQueryString;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogApiUnitTest.UI.Validators.ProductQueryString
{
    public class MinPriceKeyQueryStringValidatorTest
    {
        /**
         * case1: queryString contains unspecified key => ignore or not modify original querySTring
         * case2: queryString contains invalid value (not numeric) => remove this key and value
         * case3: queryString contains valid value => ignore or not modify original queryString 
         **/
        [Fact]
        public void Validate_UnspecifiedKey_ShouldBeIgnored()
        {
            // arrange 
            string queryStringDummy = "?notMinPrice=hey";
            string expectedResult = queryStringDummy;

            // act
            MinPriceKeyQueryStringValidator minpriceValidator = new MinPriceKeyQueryStringValidator();
            string result = minpriceValidator.Validate(queryStringDummy);

            // assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Validate_NotNumeric_ShouldBeRemoved()
        {
            // arrange 
            string queryStringDummy = "?minprice=hey&anything=else";
            string expectedResult = "anything=else";

            // act
            MinPriceKeyQueryStringValidator minpriceValidator = new MinPriceKeyQueryStringValidator();
            string result = minpriceValidator.Validate(queryStringDummy);

            // assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Validate_ValidValue_ShouldBeKept()
        {
            // arrange 
            string queryStringDummy = "?minprice=54&anything=else";
            string expectedResult = queryStringDummy;

            // act
            MinPriceKeyQueryStringValidator minpriceValidator = new MinPriceKeyQueryStringValidator();
            string result = minpriceValidator.Validate(queryStringDummy);

            // assert
            Assert.Equal(expectedResult, result);
        }
    }
}
