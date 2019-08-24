using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.UI.Utils
{
    public interface IUtil
    {
        IDictionary<string, string> MapQueryString(NameValueCollection queryString);
    }
}
