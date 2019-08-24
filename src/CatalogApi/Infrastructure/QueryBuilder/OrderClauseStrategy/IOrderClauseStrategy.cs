using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.QueryBuilder.OrderClauseStrategy
{
    public interface IOrderClauseStrategy
    {
        IQueryable<Product> GetOrderClause(IQueryable<Product> query);
    }
}
