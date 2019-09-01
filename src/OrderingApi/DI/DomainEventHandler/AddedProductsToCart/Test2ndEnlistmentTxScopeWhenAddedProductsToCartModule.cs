using Autofac;
using Autofac.Extras.DynamicProxy;
using MediatR;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Application.DomainEventHandler.AddedProductsToCart;
using OrderingApi.Application.DomainEventHandler.CartCreated;
using OrderingApi.Config.AOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.DomainEventHandler.AddedProductsToCart
{
    public class Test2ndEnlistmentTxScopeWhenAddedProductsToCartDomainEventHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Test2ndEnlistmentTxScopeWhenAddedProductsToCart>()
                .As<INotificationHandler<AddedProductsToCartDomainEvent>>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerDependency();

        }
    }
}
