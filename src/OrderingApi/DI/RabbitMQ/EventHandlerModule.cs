using Autofac;
using Microsoft.Extensions.Configuration;
using OrderingApi.Infrastructure.RabbitMQ.Config;
using OrderingApi.Infrastructure.RabbitMQ.Config.EventHandler;
using OrderingApi.Infrastructure.RabbitMQ.Sender;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.RabbitMQ
{
    public class EventHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // "pc acks" event handler
            builder.RegisterType<UpdateDomainEventStatusToSuccessWhenPublisherReceivedConfirmAcksFromBroker>()
                .SingleInstance();

            // "pc nacks" event handler
            builder.RegisterType<UpdateDomainEventStatusToFailureWhenPublisherReceivedConfirmNacksFromBroker>()
                .SingleInstance();

            // "basic return" event handler
            builder.RegisterType<StoreUnroutableMessageWhenPublisherGetReturnedMessageFromBroker>()
                .SingleInstance();
        }
    }
}
