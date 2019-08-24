using Autofac;
using Autofac.Extras.DynamicProxy;
using MediatR;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Application.DomainEventHandler.CartCreated;
using OrderingApi.Config.AOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.DomainEventHandler
{
    public class CartCreatedDomainEventHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AssignCartToUserWhenCartCreated>()
                .As<INotificationHandler<CartCreatedDomainEvent>>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerDependency();

        }
    }
}
