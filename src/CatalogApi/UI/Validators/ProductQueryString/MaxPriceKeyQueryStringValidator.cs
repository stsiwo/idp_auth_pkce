using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CatalogApi.UI.Validators.ProductQueryString
{
    public class MaxPriceKeyQueryStringValidator : IProductQueryStringValidator

    {
        public string Validate(string queryString)
        {
            var parsedQueryString = HttpUtility.ParseQueryString(queryString);

            var maxPriceValue = parsedQueryString[QueryConstants.MaxPrice];

            // if maxPrice key does not exists, return queryString as it is
            if (maxPriceValue == null) return queryString;

            // check maxPrice value is numeric
            if (decimal.TryParse(maxPriceValue, out decimal maxPriceNumericValue))
            {
                return queryString;
            }

            // else remove it from queryString
            parsedQueryString.Remove(QueryConstants.MaxPrice);

            return parsedQueryString.ToString();

        }
    }
}
