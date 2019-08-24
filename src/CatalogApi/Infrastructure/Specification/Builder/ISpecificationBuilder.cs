using CatalogApi.Infrastructure.DataEntity;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Specification.Builder
{
    public interface ISpecificationBuilder<T> where T : IDataEntity
    {
        Expression<Func<T, bool>> Build(NameValueCollection qs);
    }
}
