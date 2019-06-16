using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.DataEntity
{
    public static class QueryStringDictionary
    {
        public static IDictionary<string, QueryStringConstants> Content = new Dictionary<string, QueryStringConstants>()
        {
            { "keyword", QueryStringConstants.KeyWord },
            { "maxprice", QueryStringConstants.MaxPrice },
            { "minprice", QueryStringConstants.MinPrice },
            { "category", QueryStringConstants.Category },
            { "subcategory", QueryStringConstants.SubCategory },
            { "reviewscore", QueryStringConstants.ReviewScore },
            { "review", QueryStringConstants.Review },
            { "sort", QueryStringConstants.Sort }
        };
    }
}
