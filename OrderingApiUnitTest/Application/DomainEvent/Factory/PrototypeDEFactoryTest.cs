using Autofac;
using Autofac.Features.Variance;
using MediatR;
using OrderingApi.Application.Command;
using OrderingApi.Application.CommandHandler;
using OrderingApi.Application.DomainEvent.Factory;
using OrderingApi.UI.Model;
using OrderingApiUnitPrototypeDEFactoryTest.Application.DomainEvent.Factory.Storage;
using OrderingApiUnitTest.Application.DomainEvent.Factory.Storage;
using OrderingApiUnitTest.Application.DomainEvent.Factory.Storage.ContravarianceStorage.PrototypeDEFactory;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace OrderingApiUnitTest.Application.DomainEvent.Factory
{
    public class PrototypeDEFactoryTest
    {
        private readonly ITestOutputHelper _output;

        public PrototypeDEFactoryTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Autofac_PrototypeDEFactory_ShouldBeResolvedProperly()
        {
            var builder = new ContainerBuilder();

            builder.RegisterSource(new ContravariantRegistrationSource());
            

            builder.RegisterType<DEaFactory>()
                .Keyed<IDEFac<ICommandT, IModelT>>(0)
                .InstancePerDependency(); 

            builder.RegisterType<DEbFactory>()
                .Keyed<IDEFac<ICommandT, IModelT>>(1)
                .InstancePerDependency(); 

            builder.RegisterType<PrototypeDEFactoryTestClass>()
                .InstancePerDependency();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                //var testm = new PrototypeDEFactoryTestClass()
                var test = scope.Resolve<PrototypeDEFactoryTestClass>();

                _output.WriteLine(test.Test(new DEbCommand(), new DEbModel(), 1).ToString());

                Assert.IsType<DEb>(test.Test(new DEbCommand(), new DEbModel(), 1));
            }
        }
    }
}
