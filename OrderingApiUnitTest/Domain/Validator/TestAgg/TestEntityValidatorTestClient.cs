using OrderingApi.Domain.TestAgg;
using OrderingApi.Domain.Validator;
using OrderingApi.Domain.Validator.FieldValidationRule.TestAgg;
using OrderingApi.Domain.Validator.Notification;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.Domain.Validator.TestAgg
{
    class TestEntityValidatorTestClient
    {
        private readonly IEntityValidator<Test, ITestFieldValidationRule> _entityValidator;
        private readonly IValidationNotification _notification;

        public TestEntityValidatorTestClient(IEntityValidator<Test, ITestFieldValidationRule> entityValidator, IValidationNotification notification)
        {
            _entityValidator = entityValidator;
            _notification = notification;
        }

        public IValidationNotification TestValidate(Test test)
        {
            return _entityValidator.Validate(test);
        } 
    }
}
