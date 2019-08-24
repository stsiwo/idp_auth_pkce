using OrderingApi.Domain.TestAgg;
using OrderingApi.Domain.Validator.Specification.TestAgg.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator.Specification.Builder.TestAgg
{
    public class TestPasswordSpecificationBuilder : SpecificationBuilderBase<Test, ITestPasswordSpecification>
    {
        public TestPasswordSpecificationBuilder(IEnumerable<ITestPasswordSpecification> testPasswordSpecifications) : base(testPasswordSpecifications)
        {
        }
    }
}
