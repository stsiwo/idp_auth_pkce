using OrderingApi.Domain.Base;
using OrderingApi.Domain.Validator.Specification.Builder;
using OrderingApi.Domain.Validator.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator.FieldValidationRule
{
    public class FieldValidationRuleBase<T> : IFieldValidationRule<T> 
        where T : IEntity
    {
        private readonly string _message;

        private readonly string _field;

        private readonly ISpecification<T> _specification;

        public FieldValidationRuleBase(string message, string field, ISpecification<T> specification)
        {
            _message = message;
            _field = field;
            _specification = specification;
        }

        public bool Validate(T e)
        {
            return _specification.IsSatisfiedBy(e);
        }

        public string GetMessage()
        {
            return _message;
        }

        public string GetField()
        {
            return _field;
        }
    }
}
