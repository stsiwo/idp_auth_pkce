using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.QueryBuilder.OrderClauseStrategy
{
    public class PriceAscStrategy : IOrderClauseStrategy
    {
        public IQueryable<Product> GetOrderClause(IQueryable<Product> query)
        {
            return query.OrderBy(p => p.Price);
        }
    }
}
