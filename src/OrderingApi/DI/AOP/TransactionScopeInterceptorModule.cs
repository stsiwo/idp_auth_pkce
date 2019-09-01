using Autofac;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderingApi.Application.DomainEvent.Factory;
using OrderingApi.Config.AOP;
using OrderingApi.Infrastructure.MSTransactionScope;
using OrderingApi.Infrastructure.RabbitMQ.Sender;

namespace OrderingApi.DI.AOP
{
    public class TransactionScopeInterceptorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
//            builder.Register(c => new TransactionScopeInterceptor(
//                    c.Resolve<ILogger<TransactionScopeInterceptor>>(),
//                    c.Resolve<DispatchIntegrationEventWhenTransactionCompletedEvent>(),
//                    c.Resolve<IRmqSender>(),
//                    c.Resolve<IMediator>(),
//                    // here is the problem 
//                    // tried to resolve IIndex but there is no way to explicitly do this so create wrapper class
//                    // so wrapper class has IIndex dep and its resolved implicitly
//                    c.Resolve<DomainEventFactoryWrapper>()
//                ))
//                .InstancePerDependency();
        }
    }
}
