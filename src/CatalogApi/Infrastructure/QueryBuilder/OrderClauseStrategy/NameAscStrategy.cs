using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogApi.Infrastructure.DataEntity;

namespace CatalogApi.Infrastructure.QueryBuilder.OrderClauseStrategy
{
    public class NameAscStrategy : IOrderClauseStrategy
    {
        
        public IQueryable<Product> GetOrderClause(IQueryable<Product> query)
        {
            return query.OrderBy(p => p.Name);
        }
    }
}
