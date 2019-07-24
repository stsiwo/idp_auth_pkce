using OrderingApi.Domain.Base;
using OrderingApi.Domain.Validator.FieldValidationRule;
using OrderingApi.Domain.Validator.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator
{
    public interface IEntityValidator<T, F> 
        where T : IEntity
        where F : IFieldValidationRule<T>
    {
        IValidationNotification Validate(T e);
    }
}
