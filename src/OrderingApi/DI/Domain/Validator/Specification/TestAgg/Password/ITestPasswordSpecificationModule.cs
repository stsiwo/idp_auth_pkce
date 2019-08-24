using Autofac;
using OrderingApi.Domain.Validator.Specification.TestAgg.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.Domain.Validator.Specification.TestAgg.Password
{
    public class ITestPasswordSpecificationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HasNumericSpecification>().As<ITestPasswordSpecification>();
            builder.RegisterType<HasSpecialCharSpecification>().As<ITestPasswordSpecification>();

        }
    }
}
