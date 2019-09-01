using Autofac;
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
        public void Interceptor_Test()
        {
            var builder = new ContainerBuilder();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {

                _output.WriteLine("finisning test");

                Assert.True(false);
            }

        }
    }
}
