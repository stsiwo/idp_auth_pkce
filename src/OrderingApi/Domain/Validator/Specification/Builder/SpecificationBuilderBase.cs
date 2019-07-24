using OrderingApi.Domain.Base;
using OrderingApi.Domain.Validator.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator.Specification.Builder
{
    public class SpecificationBuilderBase<T, S> : ISpecificationBuilder<T, S>
        where T : IEntity
        where S : ISpecification<T>
    {
        IEnumerable<S> _specificationList;
        ISpecification<T> _baseSpecification;

        public SpecificationBuilderBase(IEnumerable<S> specificationList)
        {
            _specificationList = specificationList;

            // assign the first specification as base specification
            _baseSpecification = _specificationList.FirstOrDefault();
        }

        public ISpecification<T> Build()
        {
            // add the rest of specification in the list with "And" method 
            foreach (var specification in _specificationList.Skip(1))
            {
                _baseSpecification = _baseSpecification.And(specification);
            }

            return _baseSpecification;
        }
    }
}
