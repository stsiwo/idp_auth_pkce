using Autofac;
using Microsoft.Extensions.Configuration;
using OrderingApi.Infrastructure.RabbitMQ.Config;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.RabbitMQ
{
    public class ConsumerConnectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // connection for publishing 
            builder.Register(c => c.ResolveKeyed<ConnectionFactory>(ConnectionTypeConstants.Consumer).CreateConnection())
                .Keyed<IConnection>(ConnectionTypeConstants.Consumer)
                .SingleInstance();

            // consumer channel config: 
            builder.RegisterType<ConsumerChannelConfig>()
                .SingleInstance();

            // consumer channel (singleton at main thread) 
            builder.Register(c => c.Resolve<ConsumerChannelConfig>().Configure())
                .Keyed<IModel>(ConnectionTypeConstants.Consumer)
                .SingleInstance();


            // callbacks
            // => start consumer channel when app started (technically, when Autofac container is built)
            builder.RegisterBuildCallback(c => c.ResolveKeyed<IModel>(ConnectionTypeConstants.Consumer));

        }
    }
}
