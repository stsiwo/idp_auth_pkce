using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CatalogApi.UI.Validators.ProductQueryString
{
    public class CategoryKeyQueryStringValidator : IProductQueryStringValidator
    {
        public string Validate(string queryString)
        {
            // "HttpUtility.ParseQueryString" : this is not case sensitive (like "Category" is treated as "category" or "Category")
            var parsedQueryString = HttpUtility.ParseQueryString(queryString);

            var categoryValue = parsedQueryString[QueryConstants.Category];

            // if category key does not exists, return queryString as it is
            if (categoryValue == null) return queryString;

            // check category value is numeric
            if (int.TryParse(categoryValue, out int categoryNumericValue))
            {
                // check category numeric value is within the range the category defined
                int totalCategoryNumbers = Enum.GetNames(typeof(CategoryConstants)).Length;

                if (Enumerable.Range(0, totalCategoryNumbers).Contains(categoryNumericValue))
                {
                    return queryString;
                }
            }

            // else remove it from queryString
            parsedQueryString.Remove(QueryConstants.Category);

            return parsedQueryString.ToString();
        }
    }
}
