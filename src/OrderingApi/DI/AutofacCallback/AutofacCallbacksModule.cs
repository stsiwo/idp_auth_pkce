using Autofac;
using OrderingApi.Infrastructure.RabbitMQ.Config;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.AutofacCallback
{
    public class AutofacCallbacksModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // #TEST : comment out callbacks when integration testing

            // register all callbacks when builder is built of autofac

            // => start consumer channel when app started (technically, when Autofac container is built)
            //builder.RegisterBuildCallback(c => c.ResolveKeyed<IModel>(ConnectionTypeConstants.Consumer));
        }
    }
}
