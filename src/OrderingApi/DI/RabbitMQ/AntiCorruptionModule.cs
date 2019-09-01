using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.Extensions.Configuration;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Config.AOP;
using OrderingApi.Infrastructure.RabbitMQ.Config;
using OrderingApi.Infrastructure.RabbitMQ.Config.AntiCorruption;
using OrderingApi.Infrastructure.RabbitMQ.Config.AntiCorruption.Translator;
using OrderingApi.Infrastructure.RabbitMQ.Sender;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.RabbitMQ
{
    public class AntiCorruptionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // domain event translator (IIndex: keyed search lookup)
            builder.RegisterType<CreatedCartDomainEventTranslator>()
                .Keyed<IDomainEventTranslator>(DomainEventTypeConstants.CreatedCartDomainEvent)
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerDependency();

            // domain event adapter 
            builder.RegisterType<DomainEventAdapter>()
                .As<IDomainEventAdapter>()
                .InstancePerDependency();

        }
    }
}
