using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.UI.Validators.ProductQueryString
{
    public interface IProductQueryStringValidator
    {
        string Validate(string queryString);
    }
}
