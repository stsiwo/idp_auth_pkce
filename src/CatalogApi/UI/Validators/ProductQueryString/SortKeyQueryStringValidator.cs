using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CatalogApi.UI.Validators.ProductQueryString
{
    public class SortKeyQueryStringValidator : IProductQueryStringValidator
    {
        public string Validate(string queryString)
        {
            var parsedQueryString = HttpUtility.ParseQueryString(queryString);

            var sortValue = parsedQueryString[QueryConstants.Sort];

            // if sort key does not exists, return queryString as it is
            if (sortValue == null) return queryString;

            // check sort value is numeric
            if (int.TryParse(sortValue, out int sortNumericValue))
            {
                // check sort numeric value is within the range the sort defined
                int totalSortNumbers = Enum.GetNames(typeof(SortConstants)).Length;

                if (Enumerable.Range(0, totalSortNumbers).Contains(sortNumericValue))
                {
                    return queryString;
                }
            }

            // else remove it from queryString
            parsedQueryString.Remove(QueryConstants.Sort);

            return parsedQueryString.ToString();

        }
    }
}
