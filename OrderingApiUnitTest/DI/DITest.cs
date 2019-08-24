using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher;
using OrderingApiUnitTest.DI.AOP;
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

            // EnableInterfaceInterceptors : creates an interface proxy that performs the interception (need method to be public in IF)
            // EnableClassInterceptors: dynamically subclasses teh target component perform interception (need method to be "virtual")

            // individual interceptor (there is no way to apply interceptor for all component automatically)
            // 1. register as usual component
            // 2. in a target component, use interceptedBy
            //            builder.Register(c => new LogInterceptor(_output));
            //
            //            builder.RegisterType<TargetEntity>()
            //                .As<ITargetEntity>()
            //                .EnableInterfaceInterceptors()
            //                .InterceptedBy(typeof(LogInterceptor))
            //                .InstancePerDependency();


            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {

                ITargetEntity entity = scope.Resolve<ITargetEntity>();
                _output.WriteLine(entity.TestMethod());

                Assert.True(false);
            }
        }
    }
}
