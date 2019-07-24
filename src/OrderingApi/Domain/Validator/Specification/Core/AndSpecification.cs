using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator.Specification.Core
{
    public class AndSpecification<T> : SpecificationBase<T>
    {
        ISpecification<T> _left;
        ISpecification<T> _right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;

        }
        public override bool IsSatisfiedBy(T e)
        {
            return _left.IsSatisfiedBy(e) && _right.IsSatisfiedBy(e);
        }
    }
}
