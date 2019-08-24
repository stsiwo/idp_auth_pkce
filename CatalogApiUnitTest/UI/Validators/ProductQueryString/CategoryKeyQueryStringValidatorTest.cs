using CatalogApi.UI.Validators.ProductQueryString;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogApiUnitTest.UI.Validators.ProductQueryString
{
    public class CategoryKeyQueryStringValidatorTest
    {
        /**
         * case1: queryString contains unspecified key => ignore or not modify original querySTring
         * case2: queryString contains invalid value (not numeric) => remove this key and value
         * case3: (edge case) queryString contains invalid value (numeric but not within the range) => remove this key and value
         * case4: (edge case) queryString contains valid value => ignore or not modify original queryString 
         **/
        [Fact]
        public void Validate_UnspecifiedKey_ShouldBeIgnored()
        {
            // arrange 
            string queryStringDummy = "?notCategory=hey";
            string expectedResult = queryStringDummy;

            // act
            CategoryKeyQueryStringValidator categoryValidator = new CategoryKeyQueryStringValidator();
            string result = categoryValidator.Validate(queryStringDummy);

            // assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Validate_NotNumeric_ShouldBeRemoved()
        {
            // arrange 
            string queryStringDummy = "?category=hey&anything=else";
            string expectedResult = "anything=else";

            // act
            CategoryKeyQueryStringValidator categoryValidator = new CategoryKeyQueryStringValidator();
            string result = categoryValidator.Validate(queryStringDummy);

            // assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Validate_NumericButNotWithinRange_ShouldBeRemoved_EdgeCase()
        {
            // arrange 
            string queryStringDummy = "?category=10&anything=else";
            string expectedResult = "anything=else";

            // act
            CategoryKeyQueryStringValidator categoryValidator = new CategoryKeyQueryStringValidator();
            string result = categoryValidator.Validate(queryStringDummy);

            // assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Validate_ValidValue_ShouldBeKept_EdgeCase()
        {
            // arrange 
            string queryStringDummy = "?category=9&anything=else";
            string expectedResult = queryStringDummy;

            // act
            CategoryKeyQueryStringValidator categoryValidator = new CategoryKeyQueryStringValidator();
            string result = categoryValidator.Validate(queryStringDummy);

            // assert
            Assert.Equal(expectedResult, result);
        }
    }
}
