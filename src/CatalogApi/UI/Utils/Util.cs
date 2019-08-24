using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace CatalogApi.UI.Utils
{
    public class Util : IUtil
    {
        public IDictionary<string, string> MapQueryString(NameValueCollection queryString)
        {
            IDictionary<string, string> qsDictionary = new Dictionary<string, string>();

            foreach (var key in queryString.AllKeys)
            {
                qsDictionary.Add(key, queryString[key]);
            }

            return qsDictionary;
        }
    }
}
