using CatalogApi.Infrastructure.DataEntity;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.QueryBuilder
{
    public interface IQueryBuilder<T>
    where T : IDataEntity
    {
        Task<IList<T>> Build(IDictionary<string, string> qs = null);

    }
}
