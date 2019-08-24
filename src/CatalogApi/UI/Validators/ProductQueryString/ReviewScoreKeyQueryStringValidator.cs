using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CatalogApi.UI.Validators.ProductQueryString
{
    public class ReviewScoreKeyQueryStringValidator : IProductQueryStringValidator
    {
        public string Validate(string queryString)
        {
            // total numbers of review
            var parsedQueryString = HttpUtility.ParseQueryString(queryString);

            var reviewScoreValue = parsedQueryString[QueryConstants.ReviewScore];

            // if reviewScore key does not exists, return queryString as it is
            if (reviewScoreValue == null) return queryString;

            // check review score value is numeric
            if (int.TryParse(reviewScoreValue, out int reviewScoreNumericValue))
            {
                // check review score numeric value is within the range the review score defined
                int totalReviewScoreNumbers = Enum.GetNames(typeof(ReviewScoreConstants)).Length;

                if (Enumerable.Range(0, totalReviewScoreNumbers).Contains(reviewScoreNumericValue))
                {
                    return queryString;
                }
            }

            // else remove it from queryString
            parsedQueryString.Remove(QueryConstants.ReviewScore);

            return parsedQueryString.ToString();
        }
    }
}
