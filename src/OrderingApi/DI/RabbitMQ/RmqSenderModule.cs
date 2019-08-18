using Autofac;
using Microsoft.Extensions.Configuration;
using OrderingApi.Infrastructure.RabbitMQ.Config;
using OrderingApi.Infrastructure.RabbitMQ.Sender;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.RabbitMQ
{
    public class RmqSenderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // sender
            builder.RegisterType<RmqSender>()
                .As<IRmqSender>()
                .InstancePerDependency();
        }
    }
}
