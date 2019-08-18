using Autofac;
using OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher;
using OrderingApiUnitTest.DI.TestEntity;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace OrderingApiUnitTest.DI
{
    public class DITest
    {
        private readonly ITestOutputHelper _output;

        public DITest(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public void DI_Test()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<OrderingApiPublisher>()
                .As<IRmqPublisher>()
                .SingleInstance();

            builder.RegisterType<CatalogApiPublisher>()
                .As<IRmqPublisher>()
                .SingleInstance();

            builder.RegisterType<IdentityApiPublisher>()
                .As<IRmqPublisher>()
                .SingleInstance();

            builder.RegisterType<IPublisherTest>()
                .SingleInstance();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                var pub = scope.Resolve<IPublisherTest>();

                _output.WriteLine(pub.Count().ToString());

                Assert.True(false);
            }

        }
    }
}
