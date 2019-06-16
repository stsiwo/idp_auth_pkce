using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.DataEntity
{
    public enum QueryStringConstants
    {
        KeyWord = 0,
        MaxPrice = 1,
        MinPrice = 2,
        Category = 3,
        SubCategory = 4,
        ReviewScore = 5,
        Review = 6,
        Sort = 7,
    }
}