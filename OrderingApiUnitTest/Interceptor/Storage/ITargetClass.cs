using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderingApiUnitTest.Interceptor.Storage
{
    public interface ITargetClass
    {
        Task<string> Run();
    }
}
