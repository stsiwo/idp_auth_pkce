using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CatalogApi.UI.Validators.ProductQueryString
{
    public class KeyWordKeyQueryStringValidator : IProductQueryStringValidator
    {
        public string Validate(string queryString)
        {
            // for now, i don't think i need to validate KeyWord key and its value
            // if you come up with something, please write validator here...
            return queryString; 
        }
    }
}
