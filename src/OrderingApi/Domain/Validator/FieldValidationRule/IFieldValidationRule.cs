using OrderingApi.Domain.Base;
using OrderingApi.Domain.Validator.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator.FieldValidationRule
{
    public interface IFieldValidationRule<T> 
        where T : IEntity
    {
        bool Validate(T e);

        string GetMessage();

        string GetField();
    }
}
