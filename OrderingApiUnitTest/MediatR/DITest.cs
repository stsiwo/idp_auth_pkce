using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using MediatR;
using OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher;
using OrderingApiUnitTest.MediatR.TestEntity;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace OrderingApiUnitTest.MediatR
{
    public class MediatRTest
    {
        private readonly ITestOutputHelper _output;

        public MediatRTest(ITestOutputHelper output)
        {
            _output = output;
        }

        /**
         * polymorphic dispatching does not work (make an event handler subscribe all event using INotification)
         *  - see more info : https://github.com/jbogard/MediatR.Extensions.Microsoft.DependencyInjection/issues/24
         */
        [Fact]
        public void MediatR_SubscribeAllEvent_Test()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            // request & notification handlers
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterType<DEHandler>()
                .AsImplementedInterfaces()
                .WithParameter(new TypedParameter(typeof(ITestOutputHelper), _output))
                .InstancePerDependency();

            builder.RegisterType<DEHandler1>()
                .AsImplementedInterfaces()
                .WithParameter(new TypedParameter(typeof(ITestOutputHelper), _output))
                .InstancePerDependency();

            builder.RegisterType<TestClass>()
                .InstancePerDependency();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                var test = scope.Resolve<TestClass>();
                test.Dispatch();

                _output.WriteLine("finisning test");

                Assert.True(false);
            }
        }
    }
}
