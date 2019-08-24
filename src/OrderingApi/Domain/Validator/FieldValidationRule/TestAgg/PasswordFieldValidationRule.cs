using OrderingApi.Domain.TestAgg;
using OrderingApi.Domain.Validator.Specification.Builder;
using OrderingApi.Domain.Validator.Specification.TestAgg.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator.FieldValidationRule.TestAgg
{
    public class PasswordFieldValidationRule : FieldValidationRuleBase<Test>, ITestFieldValidationRule
    {
        public PasswordFieldValidationRule(string message, string field, ISpecificationBuilder<Test, ITestPasswordSpecification> specificationBuilder)
            : base(message, field, specificationBuilder.Build())
        {

        }
    }
}
