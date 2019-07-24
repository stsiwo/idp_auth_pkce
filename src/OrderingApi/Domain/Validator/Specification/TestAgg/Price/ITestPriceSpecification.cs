using OrderingApi.Domain.TestAgg;
using OrderingApi.Domain.Validator.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator.Specification.TestAgg.Price
{
    public interface ITestPriceSpecification : ISpecification<Test>
    {
        // marker interface for resolving speficiations for test entity's price field
    }
}
