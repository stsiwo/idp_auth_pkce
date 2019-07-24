using OrderingApi.Domain.TestAgg;
using OrderingApi.Domain.Validator.FieldValidationRule.TestAgg;
using OrderingApi.Domain.Validator.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator.TestAgg
{
    public class TestEntityValidator : EntityValidatorBase<Test, ITestFieldValidationRule>
    {
        public TestEntityValidator(IEnumerable<ITestFieldValidationRule> testFieldValidationRules, IValidationNotification validationNotification) 
            : base(testFieldValidationRules, validationNotification)
        {

        }
    }
}
