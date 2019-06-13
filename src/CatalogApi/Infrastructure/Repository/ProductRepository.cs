using AutoMapper;
using CatalogApi.Application.DTO;
using CatalogApi.Application.Repository;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.QueryBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Repository
{
    public class ProductRepository : IRepository<Product, ProductDTO>
    {
        // inject per request 
        private readonly IMapper _mapper;

        // inject per request
        private readonly IQueryBuilder<Product> _queryBuilder;

        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(IMapper mapper, IQueryBuilder<Product> queryBuilder, ILogger<ProductRepository> logger)
        {
            _mapper = mapper;
            _queryBuilder = queryBuilder;
            _logger = logger;
        }


        public async Task<IList<ProductDTO>> GetList(IDictionary<string, string> qs)
        {
            List<Product> results = await _queryBuilder.Build(qs);

            _logger.LogDebug("result of product query satoshi: {@Result}", results);

            // 2. assign results to List<Product> DTO
            IList<ProductDTO> products = this._mapper.Map<IList<Product>, IList<ProductDTO>>(results);

            // 3. return it!!
            return products;
        }
    }
}
