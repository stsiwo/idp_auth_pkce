using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace OrderingApiUnitTest.Interceptor.Storage
{
    // manual async implementation. for now, works fine but need to some error handling
    public class TestInterceptor : IInterceptor
    {
        private readonly ITestOutputHelper _output;

        public TestInterceptor(ITestOutputHelper output)
        {
            _output = output;
        }
        public void Intercept(IInvocation invocation)
        {
            _output.WriteLine("start intercepting ...");

            invocation.Proceed();
            var task = (Task<string>)invocation.ReturnValue;
            invocation.ReturnValue = task.ContinueWith((t) =>
            {
                var modified = t.Result + " is modified";
                _output.WriteLine("ending intercepting ...");
                return modified;
            });
        }
    }
}
