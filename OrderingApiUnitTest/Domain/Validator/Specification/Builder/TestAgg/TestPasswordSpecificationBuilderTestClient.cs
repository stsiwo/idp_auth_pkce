using OrderingApi.Domain.TestAgg;
using OrderingApi.Domain.Validator.Specification.Builder;
using OrderingApi.Domain.Validator.Specification.TestAgg.Password;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.Domain.Validator.Specification.Builder.TestAgg
{
    class TestPasswordSpecificationBuilderTestClient
    {
        private ISpecificationBuilder<Test, ITestPasswordSpecification> _testPasswordSpecificationBuilder;

        public TestPasswordSpecificationBuilderTestClient(ISpecificationBuilder<Test, ITestPasswordSpecification> specificationBuilder)
        {
            _testPasswordSpecificationBuilder = specificationBuilder;
        }

        public bool TestValidate(Test test)
        {
            return _testPasswordSpecificationBuilder.Build().IsSatisfiedBy(test);
        } 
    }
}
