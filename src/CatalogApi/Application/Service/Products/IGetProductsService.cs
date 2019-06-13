using CatalogApi.Application.DTO;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Application.Service.Products
{
    public interface IGetProductsService
    {
        Task<IList<ProductDTO>> GetProducts(IDictionary<string, string> qs);
    }
}
