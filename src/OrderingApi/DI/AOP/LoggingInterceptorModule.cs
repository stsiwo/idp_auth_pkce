using Autofac;
using OrderingApi.Config.AOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.AOP
{
    public class LoggingInterceptorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new LoggingInterceptor());
        }
    }
}
