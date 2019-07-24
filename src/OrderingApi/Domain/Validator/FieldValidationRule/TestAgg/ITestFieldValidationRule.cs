using OrderingApi.Domain.TestAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator.FieldValidationRule.TestAgg
{
    public interface ITestFieldValidationRule : IFieldValidationRule<Test>
    {
        // marker interface for resolve validation rules for test entity
    }
}
