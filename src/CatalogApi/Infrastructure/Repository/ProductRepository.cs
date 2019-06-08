using AutoMapper;
using CatalogApi.Application.DTO;
using CatalogApi.Application.Repository;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.QueryBuilder;
using Microsoft.EntityFrameworkCore;
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

        public ProductRepository(IMapper mapper, IQueryBuilder<Product> queryBuilder)
        {
            _mapper = mapper;
            _queryBuilder = queryBuilder;
        }


        public async Task<List<ProductDTO>> GetList(IDictionary<string, string> qs)
        {
            List<DataEntity.Product> results = await _queryBuilder.Build(qs);

            // 2. assign results to List<Product> DTO
            List<ProductDTO> products = this._mapper.Map<List<ProductDTO>>(results);

            // 3. return it!!
            return products;
        }
    }
}
