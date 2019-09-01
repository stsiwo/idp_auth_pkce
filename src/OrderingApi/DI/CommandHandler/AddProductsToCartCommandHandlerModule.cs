using Autofac;
using Autofac.Extras.DynamicProxy;
using MediatR;
using OrderingApi.Application.CommandHandler;
using OrderingApi.Config.AOP;
using OrderingApi.Application.Command;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderingApi.Application.Repository;
using OrderingApi.Domain.CartAgg;
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using OrderingApi.Infrastructure.MSTransactionScope;
using OrderingApi.Infrastructure.RabbitMQ.Sender;
using OrderingApi.Application.DomainEvent.Factory;

namespace OrderingApi.DI.CommandHandler
{
    public class AddProductsToCartCommandHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => 
            {
                // target class
                var myClass = new AddProductsToCartCommandHandler(c.Resolve<IRepository<Cart>>());
                // interceptor
                var interceptor = new TransactionScopeInterceptor(
                    c.Resolve<ILogger<TransactionScopeInterceptor>>(),
                    c.Resolve<DispatchIntegrationEventWhenTransactionCompletedEvent>(),
                    c.Resolve<IRmqSender>(),
                    c.Resolve<IMediator>(),
                    // here is the problem 
                    // tried to resolve IIndex but there is no way to explicitly do this so create wrapper class
                    // so wrapper class has IIndex dep and its resolved implicitly
                    c.Resolve<DomainEventFactoryWrapper>()
                    );
                // proxy
                var generator = new ProxyGenerator();
                return generator.CreateInterfaceProxyWithTargetInterface<IRequestHandler<AddProductsToCartCommand, CartModel>>(myClass, interceptor);
            })
                .As<IRequestHandler<AddProductsToCartCommand, CartModel>>()
                .InstancePerDependency();
        }
    }
}
