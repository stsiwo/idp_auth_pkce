using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingApiUnitTest.Interceptor.Storage
{
    class TargetClass : ITargetClass
    {
        public async virtual Task<string> Run()
        {
            await Task.Delay(1000);

            return "hey"; 
        }
    }
}
