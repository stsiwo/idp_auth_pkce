using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Builder;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.QueryBuilder
{
    public class ProductQueryBuilder : IQueryBuilder<Product>
    {
        private readonly CatalogApiDbContext _context;

        private readonly ISpecificationBuilder<Product> _specificationBuilder;

        private IQueryable<Product> _query;

        public ProductQueryBuilder(CatalogApiDbContext context, ISpecificationBuilder<Product> specificationBuilder)
        {
            _context = context;
            _specificationBuilder = specificationBuilder;
        }
        public async Task<List<Product>> Build(IDictionary<string, string> qs = null)
        {
            _query = _context.Products
                .Include(p => p.SubImages)
                .Include(p => p.Reviews).AsQueryable();

            if (qs.Keys.Intersect(QueryConstants.ToList()).Count() > 0)
                _query = _query.Where(_specificationBuilder.Build(qs)).AsQueryable();

            if (qs.ContainsKey(QueryConstants.Sort))
                _query = BuildOrderByClause(qs);

            return await _query.ToListAsync();
        }

        private IQueryable<Product> BuildOrderByClause(IDictionary<string, string> qs)
        {
            {
                switch (Convert.ToInt16(qs[QueryConstants.Sort]))
                {
                    case (int)SortConstants.DateAsc:
                        return _query.OrderBy(p => p.CreationDate);
                    case (int)SortConstants.DateDesc:
                        return _query.OrderByDescending(p => p.CreationDate);
                    case (int)SortConstants.PriceAsc:
                        return _query.OrderBy(p => p.Price);
                    case (int)SortConstants.PriceDesc:
                        return _query.OrderByDescending(p => p.Price);
                    case (int)SortConstants.NameAsc:
                        return _query.OrderBy(p => p.Name);
                    case (int)SortConstants.NameDesc:
                        return _query.OrderByDescending(p => p.Name);
                    case (int)SortConstants.ReviewAsc:
                        return _query.OrderBy(p => p.Reviews.Count);
                    case (int)SortConstants.ReviewDesc:
                        return _query.OrderByDescending(p => p.Reviews.Count);
                    case (int)SortConstants.ReviewScoreAsc:
                        return _query.OrderBy(p => p.Reviews.Average(r => (int) r.Score));
                    case (int)SortConstants.ReviewScoreDesc:
                        return _query.OrderByDescending(p => p.Reviews.Average(r => (int) r.Score));
                    default:
                        // if sort value is anything else than above just return defualt (DateAsc) 
                        return _query.OrderBy(p => p.CreationDate);
                }
            }
        }
    }

}
