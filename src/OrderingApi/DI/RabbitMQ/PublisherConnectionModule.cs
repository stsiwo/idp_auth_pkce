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
    public class PublisherConnectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // connection for publishing 
            builder.Register(c => c.ResolveKeyed<ConnectionFactory>(ConnectionTypeConstants.Publisher).CreateConnection())
                .Keyed<IConnection>(ConnectionTypeConstants.Publisher)
                .SingleInstance();

            // publisher channel config: 
            builder.RegisterType<PublisherChannelConfig>()
                .SingleInstance();

            // publisher channel (per thread/request at worker thread)
            builder.Register(c => c.Resolve<PublisherChannelConfig>().Configure())
                .Keyed<IModel>(ConnectionTypeConstants.Publisher)
                .InstancePerLifetimeScope();

        }
    }
}
