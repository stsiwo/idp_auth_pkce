using CatalogApi.Infrastructure.DataEntity;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.QueryBuilder
{
    public interface IQueryBuilder<T>
    where T : IDataEntity
    {
        Task<IList<T>> Build(NameValueCollection qs = null);

    }
}
