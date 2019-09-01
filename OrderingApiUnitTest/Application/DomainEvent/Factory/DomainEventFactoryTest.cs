using Autofac;
using Autofac.Features.Variance;
using MediatR;
using OrderingApi.Application.Command;
using OrderingApi.Application.CommandHandler;
using OrderingApi.Application.DomainEvent.Factory;
using OrderingApi.UI.Model;
using OrderingApiUnitTest.Application.DomainEvent.Factory.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace OrderingApiUnitTest.Application.DomainEvent.Factory
{
    public class DomainEventFactoryTest
    {
        private readonly ITestOutputHelper _output;

        public DomainEventFactoryTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Autofac_DomainEventFactory_ShouldBeResolvedProperly()
        {
            var builder = new ContainerBuilder();

            builder.RegisterSource(new ContravariantRegistrationSource());
            

            builder.RegisterType<AddedProductsToCartDomainEventFactory>()
                .Keyed<IDomainEventFactory>(typeof(AddProductsToCartCommandHandler))
                .InstancePerDependency();

            builder.RegisterType<TestClass>()
                .InstancePerDependency();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                var test = scope.Resolve<TestClass>();

                _output.WriteLine(test.Test());

                Assert.True(false);
            }
        }
    }
}
