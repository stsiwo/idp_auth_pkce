using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.DataEntity
{
    public enum SortConstants
    {
        DateAsc = 0,
        DateDesc = 1,
        PriceAsc = 2,
        PriceDesc = 3,
        NameAsc = 4,
        NameDesc = 5,
        ReviewAsc = 6,
        ReviewDesc = 7,
        ReviewScoreAsc = 8,
        ReviewScoreDesc = 9,
    }
}
