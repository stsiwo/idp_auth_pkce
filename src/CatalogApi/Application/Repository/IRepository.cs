using CatalogApi.Application.DTO;
using CatalogApi.Infrastructure.DataEntity;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Application.Repository
{
    public interface IRepository<T, R> 
        where T : IDataEntity // query 
        where R : IDTO        // result 
    {
        /**
         * return a list of DataEntity based on query string (qs)
         **/ 
        Task<IList<R>> GetList(NameValueCollection qs);
    }
}
