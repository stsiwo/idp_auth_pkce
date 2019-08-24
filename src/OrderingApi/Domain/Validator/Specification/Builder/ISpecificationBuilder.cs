using OrderingApi.Domain.Base;
using OrderingApi.Domain.Validator.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator.Specification.Builder
{
    public interface ISpecificationBuilder<T, S>
        where T : IEntity
        where S : ISpecification<T>
    {
        ISpecification<T> Build();
    }
}
