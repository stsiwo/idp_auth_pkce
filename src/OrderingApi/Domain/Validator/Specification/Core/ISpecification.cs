using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator.Specification.Core
{
    public interface ISpecification<T> : IEntity
    {
        bool IsSatisfiedBy(T e);

        ISpecification<T> And(ISpecification<T> specification);
    }
}
