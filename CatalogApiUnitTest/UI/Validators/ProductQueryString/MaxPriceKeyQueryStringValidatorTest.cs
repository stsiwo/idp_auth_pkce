using CatalogApi.UI.Validators.ProductQueryString;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogApiUnitTest.UI.Validators.ProductQueryString
{
    public class MaxPriceKeyQueryStringValidatorTest
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
            string queryStringDummy = "?notMaxPrice=hey";
            string expectedResult = queryStringDummy;

            // act
            MaxPriceKeyQueryStringValidator maxpriceValidator = new MaxPriceKeyQueryStringValidator();
            string result = maxpriceValidator.Validate(queryStringDummy);

            // assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Validate_NotNumeric_ShouldBeRemoved()
        {
            // arrange 
            string queryStringDummy = "?maxprice=hey&anything=else";
            string expectedResult = "anything=else";

            // act
            MaxPriceKeyQueryStringValidator maxpriceValidator = new MaxPriceKeyQueryStringValidator();
            string result = maxpriceValidator.Validate(queryStringDummy);

            // assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Validate_ValidValue_ShouldBeKept()
        {
            // arrange 
            string queryStringDummy = "?maxprice=54&anything=else";
            string expectedResult = queryStringDummy;

            // act
            MaxPriceKeyQueryStringValidator maxpriceValidator = new MaxPriceKeyQueryStringValidator();
            string result = maxpriceValidator.Validate(queryStringDummy);

            // assert
            Assert.Equal(expectedResult, result);
        }
    }
}
