using CatalogApi.UI.Validators.ProductQueryString;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogApiUnitTest.UI.Validators.ProductQueryString
{
    public class SubCategoryKeyQueryStringValidatorTest
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
            string queryStringDummy = "?notSubCategory=hey";
            string expectedResult = queryStringDummy;

            // act
            SubCategoryKeyQueryStringValidator subcategoryValidator = new SubCategoryKeyQueryStringValidator();
            string result = subcategoryValidator.Validate(queryStringDummy);

            // assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Validate_NotNumeric_ShouldBeRemoved()
        {
            // arrange 
            string queryStringDummy = "?subcategory=hey&anything=else";
            string expectedResult = "anything=else";

            // act
            SubCategoryKeyQueryStringValidator subcategoryValidator = new SubCategoryKeyQueryStringValidator();
            string result = subcategoryValidator.Validate(queryStringDummy);

            // assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Validate_NumericButNotWithinRange_ShouldBeRemoved_EdgeCase()
        {
            // arrange 
            string queryStringDummy = "?subcategory=55&anything=else";
            string expectedResult = "anything=else";

            // act
            SubCategoryKeyQueryStringValidator subcategoryValidator = new SubCategoryKeyQueryStringValidator();
            string result = subcategoryValidator.Validate(queryStringDummy);

            // assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Validate_ValidValue_ShouldBeKept_EdgeCase()
        {
            // arrange 
            string queryStringDummy = "?subcategory=54&anything=else";
            string expectedResult = queryStringDummy;

            // act
            SubCategoryKeyQueryStringValidator subcategoryValidator = new SubCategoryKeyQueryStringValidator();
            string result = subcategoryValidator.Validate(queryStringDummy);

            // assert
            Assert.Equal(expectedResult, result);
        }

    }
}
