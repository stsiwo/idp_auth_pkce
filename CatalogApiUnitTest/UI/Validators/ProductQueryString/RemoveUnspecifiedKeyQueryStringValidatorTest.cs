using CatalogApi.UI.Validators.ProductQueryString;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogApiUnitTest.UI.Validators.ProductQueryString
{
    public class RemoveUnspecifiedKeyQueryStringValidatorTest
    {

        [Fact]
        public void Validate_UnspecifiedKey_ShouldBeRemoved()
        {
            // arrange 
            string queryStringDummy = "category=hey&subcategory=hey&maxprice=1000&minprice=1&reviewscore=hey&unspecifiedKey=haha&sort=3";
            string expectedResult =  "maxprice=1000&minprice=1&category=hey&subcategory=hey&reviewscore=hey&sort=3";

            // act
            RemoveUnspecifiedKeyQueryStringValidator unspecifiedValidator = new RemoveUnspecifiedKeyQueryStringValidator();
            string result = unspecifiedValidator.Validate(queryStringDummy);

            // assert
            Assert.Equal(expectedResult, result);

        }
    }
}
