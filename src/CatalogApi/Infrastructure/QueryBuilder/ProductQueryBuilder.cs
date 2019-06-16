using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Builder;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using CatalogApi.Infrastructure.QueryBuilder.OrderClauseStrategy;

namespace CatalogApi.Infrastructure.QueryBuilder
{
    public class ProductQueryBuilder : IQueryBuilder<Product>
    {
        private readonly CatalogApiDbContext _context;

        private readonly ISpecificationBuilder<Product> _specificationBuilder;

        private readonly IIndex<SortConstants, IOrderClauseStrategy> _orderStrategyFactory;

        private IQueryable<Product> _query;

        public ProductQueryBuilder(CatalogApiDbContext context, IIndex<SortConstants, IOrderClauseStrategy> orderStrategyFactory, ISpecificationBuilder<Product> specificationBuilder)
        {
            _context = context;
            _orderStrategyFactory = orderStrategyFactory;
            _specificationBuilder = specificationBuilder;
        }
        public async Task<IList<Product>> Build(IDictionary<string, string> qs = null)
        {
            // include
            _query = _context.Products
                .Include(p => p.SubImages)
                .Include(p => p.Reviews).AsQueryable();

            // where clause using specification
            if (qs != null && qs.Keys.Intersect(QueryConstants.ToList()).Count() > 0)
                _query = _query.Where(_specificationBuilder.Build(qs)).AsQueryable();

            // order clause using strategy
            if (qs != null && qs.ContainsKey(QueryConstants.Sort))
            {
                int sortId = Convert.ToInt32(qs[QueryConstants.Sort]);
                IOrderClauseStrategy orderStrategy = _orderStrategyFactory[(SortConstants)sortId];

                _query = orderStrategy.GetOrderClause(_query); 
            }

            return await _query.ToListAsync();
        }

    }

}
