using OrderingApi.Domain.TestAgg;
using OrderingApi.Domain.Validator.Specification.TestAgg.Price;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator.Specification.Builder.TestAgg
{
    public class TestPriceSpecificationBuilder : SpecificationBuilderBase<Test, ITestPriceSpecification>
    {
        public TestPriceSpecificationBuilder(IEnumerable<ITestPriceSpecification> testPriceSpecifications) : base(testPriceSpecifications)
        {

        }
    }
}
