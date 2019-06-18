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
using System.Collections.Specialized;

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
        public async Task<IList<Product>> Build(NameValueCollection qs = null)
        {
            // include
            _query = _context.Products
                .Include(p => p.SubImages)
                .Include(p => p.Reviews).AsQueryable();

            // order clause using strategy
            // if qs[Sort] is not defined, assign 0 as default 
            int sortId = (qs[QueryConstants.Sort] == null) ? 0 : Convert.ToInt32(qs[QueryConstants.Sort]);

            SortConstants sortConst = (SortConstants)sortId;

            IOrderClauseStrategy orderStrategy = _orderStrategyFactory[sortConst];

            _query = orderStrategy.GetOrderClause(_query);

            // remove Sort name and value from qs 
            qs.Remove(QueryConstants.Sort);

            // where clause using specification
            // add Where clause only when qs include others rather than sort
            if (qs.Count != 0)
                _query = _query.Where(_specificationBuilder.Build(qs)).AsQueryable();

            return await _query.ToListAsync();
        }

    }

}
