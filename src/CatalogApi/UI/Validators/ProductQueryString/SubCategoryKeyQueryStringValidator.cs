using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CatalogApi.UI.Validators.ProductQueryString
{
    public class SubCategoryKeyQueryStringValidator : IProductQueryStringValidator
    {
        public string Validate(string queryString)
        {
            var parsedQueryString = HttpUtility.ParseQueryString(queryString);

            var subCategoryValue = parsedQueryString[QueryConstants.SubCategory];

            // if subcategory key does not exists, return queryString as it is
            if (subCategoryValue == null) return queryString;

            // check subCategory value is numeric
            if (int.TryParse(subCategoryValue, out int subCategoryNumericValue))
            {
                // check subCategory numeric value is within the range the subCategory defined
                int totalSubCategoryNumbers = Enum.GetNames(typeof(SubCategoryConstants)).Length;

                if (Enumerable.Range(0, totalSubCategoryNumbers).Contains(subCategoryNumericValue))
                {
                    return queryString;
                }
            }

            // else remove it from queryString
            parsedQueryString.Remove(QueryConstants.SubCategory);

            return parsedQueryString.ToString();
        }
    }
}
