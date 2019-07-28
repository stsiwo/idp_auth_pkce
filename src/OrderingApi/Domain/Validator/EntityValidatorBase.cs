using OrderingApi.Domain.Base;
using OrderingApi.Domain.Validator.FieldValidationRule;
using OrderingApi.Domain.Validator.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator
{
    public abstract class EntityValidatorBase<T, F> : IEntityValidator<T, F> 
        where T : IEntity
        where F : IFieldValidationRule<T>
    {
        private readonly IEnumerable<F> _fieldValidationRules;
        private readonly IValidationNotification _validationNotification;

        public EntityValidatorBase(IEnumerable<F> fieldValidationRules, IValidationNotification validationNotification)
        {
            _fieldValidationRules = fieldValidationRules;
            _validationNotification = validationNotification;
        }
        public IValidationNotification Validate(T e)
        {
            foreach (var rule in _fieldValidationRules)
            {
                if (!rule.Validate(e)) _validationNotification.Add(new ValidationError(rule.GetMessage(), rule.GetField()));
            }

            return _validationNotification;
        }
    }
}
