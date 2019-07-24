using OrderingApi.Domain.TestAgg;
using OrderingApi.Domain.Validator.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator.Specification.TestAgg.Password
{
    public class HasSpecialCharSpecification : SpecificationBase<Test>, ITestPasswordSpecification
    {
        public override bool IsSatisfiedBy(Test test)
        {
            return test.Password.Any(ch => !Char.IsLetterOrDigit(ch));
        }
    }
}
