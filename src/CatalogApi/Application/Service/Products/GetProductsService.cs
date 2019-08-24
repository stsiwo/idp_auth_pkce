using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using CatalogApi.Application.DTO;
using CatalogApi.Application.Repository;
using CatalogApi.Infrastructure;
using CatalogApi.Infrastructure.DataEntity;
using Microsoft.Extensions.Primitives;

namespace CatalogApi.Application.Service.Products
{
    public class GetProductsService : IGetProductsService
    {
        private readonly IRepository<Product, ProductDTO> _repository; 

        public GetProductsService(IRepository<Product, ProductDTO> repository)
        {
            _repository = repository;
        }
        public async Task<IList<ProductDTO>> GetProducts(NameValueCollection qs)
        {
            return await _repository.GetList(qs);
        }
    }
}
