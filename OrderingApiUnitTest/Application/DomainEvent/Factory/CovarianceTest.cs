using Autofac;
using Autofac.Features.Indexed;
using Autofac.Features.Variance;
using MediatR;
using OrderingApi.Application.CommandHandler;
using OrderingApi.Application.DomainEvent.Factory;
using OrderingApi.UI.Model;
using OrderingApiUnitTest.Application.DomainEvent.Factory.Storage.CovarianceStorage;
using OrderingApiUnitVarianceTest.Application.DomainEvent.Factory.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace OrderingApiUnitTest.Application.DomainEvent.Factory.Storage
{
    public class CovarianceTest
    {
        private readonly ITestOutputHelper _output;

        public CovarianceTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Contravariance_Test()
        {
//            IMealFactory<IMeal> jmealFactory = new JapaneseMealFactory();
//            IMealFactory<IMeal> kmealFactory = new KoreanMealFactory();

            var builder = new ContainerBuilder();

            //builder.RegisterSource(new ContravariantRegistrationSource());

            builder.RegisterGeneric(typeof(JapaneseMealFactory<>))
                .As(typeof(IMealFactory<>))
                .InstancePerDependency();

            builder.RegisterType<VarianceTestClass>()
                .InstancePerDependency();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                var test = scope.Resolve<VarianceTestClass>();

                _output.WriteLine(test.Test());

                Assert.True(false);
            }
        }
    }
}
