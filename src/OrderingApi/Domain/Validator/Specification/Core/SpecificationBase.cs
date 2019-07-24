using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator.Specification.Core
{
    public abstract class SpecificationBase<T> : ISpecification<T>
    {

        public abstract bool IsSatisfiedBy(T e);
        public ISpecification<T> And(ISpecification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }
    }
}
