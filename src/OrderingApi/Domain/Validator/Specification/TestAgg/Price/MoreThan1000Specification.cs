using OrderingApi.Domain.TestAgg;
using OrderingApi.Domain.Validator.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator.Specification.TestAgg.Price
{
    public class MoreThan1000Specification : SpecificationBase<Test>, ITestPriceSpecification
    {
        public override bool IsSatisfiedBy(Test e)
        {
            return e.Price > 1000.00m; 
        }
    }
}
