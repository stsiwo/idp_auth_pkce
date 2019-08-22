using Castle.DynamicProxy;
using log4net;
using Newtonsoft.Json;
using OrderingApi.Config.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Config.AOP
{
    public class LoggingInterceptor : IInterceptor
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LoggingInterceptor));

        public void Intercept(IInvocation invocation)
        {
            log.Debug("Calling Class: " + invocation.TargetType.Name);
            log.Debug("Calling Method: " + invocation.Method.Name);
            log.Debug("with parameters: " + JsonConvert.SerializeObject(invocation.Arguments, Formatting.Indented, JsonSerializerSettingsProvider.IgnoreLoopSetting));

            invocation.Proceed();

            log.Debug("Done: return value: " + invocation.ReturnValue);
        }
    }
}
