using OrderingApi.Domain.TestAgg;
using OrderingApi.Domain.Validator.Specification.Builder;
using OrderingApi.Domain.Validator.Specification.TestAgg.Price;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator.FieldValidationRule.TestAgg
{
    public class PriceFieldValidationRule : FieldValidationRuleBase<Test>, ITestFieldValidationRule
    {
        public PriceFieldValidationRule(string message, string field, ISpecificationBuilder<Test, ITestPriceSpecification> specificationBuilder)
            : base(message, field, specificationBuilder.Build())
        {

        }
    }
}
