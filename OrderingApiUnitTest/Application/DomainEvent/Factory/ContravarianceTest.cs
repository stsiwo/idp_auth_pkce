using Autofac;
using Autofac.Features.Indexed;
using Autofac.Features.Variance;
using MediatR;
using OrderingApi.Application.CommandHandler;
using OrderingApi.Application.DomainEvent.Factory;
using OrderingApi.UI.Model;
using OrderingApiUnitTest.Application.DomainEvent.Factory.Storage.ContravarianceStorage;
using OrderingApiUnitVarianceTest.Application.DomainEvent.Factory.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace OrderingApiUnitTest.Application.DomainEvent.Factory.Storage
{
    public class ContravarianceTest
    {
        private readonly ITestOutputHelper _output;

        public ContravarianceTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Contravariance_Test()
        {
            var builder = new ContainerBuilder();

            builder.RegisterSource(new ContravariantRegistrationSource());

            builder.RegisterType<SaveCommandHandler>()
                .As<ITHandler<ICommand>>()
                .InstancePerDependency(); 

//            builder.RegisterType<VarianceTestClass>()
//                .InstancePerDependency();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                var test = scope.Resolve<ITHandler<ICommand>>();

                _output.WriteLine(test.Handle(new SaveCommand()));

                Assert.True(false);
            }
        }
    }
}
