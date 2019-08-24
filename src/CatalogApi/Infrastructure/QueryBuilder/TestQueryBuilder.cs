using Autofac.Features.Indexed;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.QueryBuilder.OrderClauseStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.QueryBuilder
{
    public class TestQueryBuilder
    {
        private readonly IIndex<SortConstants, IOrderClauseStrategy> _orderStrategyFactory;

        public TestQueryBuilder(IIndex<SortConstants, IOrderClauseStrategy> orderStrategyFactory)
        {
            _orderStrategyFactory = orderStrategyFactory; 
        }

        public string Build(int type)
        {
            IOrderClauseStrategy orderStrategy = _orderStrategyFactory[(SortConstants)type];
            return orderStrategy.GetType().ToString();
        }
    }
}
