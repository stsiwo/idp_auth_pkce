using CatalogApi.Application.DTO;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Application.Service.Products
{
    public interface IGetProductsService
    {
        Task<IList<ProductDTO>> GetProducts(NameValueCollection qs);
    }
}
