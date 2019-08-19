using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace OrderingApiUnitTest.DI.AOP
{
    class LogInterceptor : IInterceptor
    {
        private readonly ITestOutputHelper _output;

        public LogInterceptor(ITestOutputHelper output)
        {
            _output = output;
        }
        public void Intercept(IInvocation invocation)
        {
            _output.WriteLine("starting log before the method call");

            invocation.Proceed();

            _output.WriteLine("the method is done. and ending log.");
        }
    }
}
