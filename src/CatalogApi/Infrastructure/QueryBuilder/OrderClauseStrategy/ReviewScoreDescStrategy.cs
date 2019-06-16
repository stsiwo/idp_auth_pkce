using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.QueryBuilder.OrderClauseStrategy
{
    public class ReviewScoreDescStrategy : IOrderClauseStrategy
    {
        public IQueryable<Product> GetOrderClause(IQueryable<Product> query)
        {
            return query.OrderByDescending(p => p.Reviews.Average(r => (int) r.Score));
        }
    }
}
