using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using OrderingApiUnitTest.Interceptor.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace OrderingApiUnitTest.Interceptor
{
    public class AutofacInterceptorTest
    {
        private readonly ITestOutputHelper _output;
        public AutofacInterceptorTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async void Interceptor_Test()
        {
            var builder = new ContainerBuilder();

                        var myClass = new TargetClass();
                        var generator = new ProxyGenerator();
                        var interceptor = new TestAsyncInterceptor();
                        ITargetClass proxy = generator.CreateInterfaceProxyWithTargetInterface<ITargetClass>(myClass, interceptor);
            
                        builder.Register(c => proxy)
                            .As<ITargetClass>()
                            .InstancePerDependency();

            builder.RegisterType<TargetClass>()
                .As<ITargetClass>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(TestInterceptor))
                .InstancePerDependency();

            builder.Register(c => new TestInterceptor(_output));

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                var test = scope.Resolve<ITargetClass>();

                var result = await test.Run();
                _output.WriteLine(result);

                Assert.True(false);
            }

        }
    }
}
