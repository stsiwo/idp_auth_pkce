using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Core;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Specification.Builder
{
    public abstract class SpecificationBuilder<T> : ISpecificationBuilder<T> where T : IDataEntity
    {
        // inject BaseSpecifcation class per dependency or per request (Autofac)
        protected ISpecification<T> _BaseSpecification;

        public SpecificationBuilder(ISpecification<T> baseSpecification)
        {
            this._BaseSpecification = baseSpecification;
        }

        public abstract Func<T, bool> Build(IDictionary<String, string> qs);
    }
}
