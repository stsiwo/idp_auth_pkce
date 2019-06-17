using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CatalogApi.UI.Validators.ProductQueryString
{
    public class MinPriceKeyQueryStringValidator : IProductQueryStringValidator
    {
        public string Validate(string queryString)
        {
            var parsedQueryString = HttpUtility.ParseQueryString(queryString);

            var minPriceValue = parsedQueryString[QueryConstants.MinPrice];

            // if minPrice key does not exists, return queryString as it is
            if (minPriceValue == null) return queryString;

            // check minPrice value is numeric
            if (decimal.TryParse(minPriceValue, out decimal minPriceNumericValue))
            {
                return queryString;
            }

            // else remove it from queryString
            parsedQueryString.Remove(QueryConstants.MinPrice);

            return parsedQueryString.ToString();
        }
    }
}
