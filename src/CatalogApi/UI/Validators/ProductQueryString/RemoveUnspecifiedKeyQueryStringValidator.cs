using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CatalogApi.UI.Validators.ProductQueryString
{
    public class RemoveUnspecifiedKeyQueryStringValidator : IProductQueryStringValidator
    {
        public string Validate(string queryString)
        {
            var parsedQueryString = HttpUtility.ParseQueryString(queryString);
            var sanitizedQueryString = HttpUtility.ParseQueryString(""); 

            // extract only defined key and ignore unspecified query key and value
            foreach (var queryKey in QueryStringDictionary.Content.Keys)
            {
                var queryValue = parsedQueryString[queryKey]; 

                if (queryValue != null)
                {
                    sanitizedQueryString.Add(queryKey, queryValue);
                }
            }

            return sanitizedQueryString.ToString();

        }
    }
}
