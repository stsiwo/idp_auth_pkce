using Autofac.Features.Indexed;
using Castle.DynamicProxy;
using log4net;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Application.DomainEvent.Factory;
using OrderingApi.Config.Global;
using OrderingApi.Infrastructure.MSTransactionScope;
using OrderingApi.Infrastructure.RabbitMQ.Sender;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace OrderingApi.Config.AOP
{
    public class TransactionScopeInterceptor : IInterceptor
    {
        private readonly ILogger<TransactionScopeInterceptor> _logger;

        private readonly DispatchIntegrationEventWhenTransactionCompletedEvent _dispatchIntegrationEventWhenTransactionCompletedEvent;

        private readonly IRmqSender _rmqSender;

        private readonly IMediator _mediator;

        private readonly IIndex<Type, IDomainEventFactory> _domainEventFactory;

        public TransactionScopeInterceptor(
            DispatchIntegrationEventWhenTransactionCompletedEvent dispatchIntegrationEventWhenTransactionCompletedEvent
            , IRmqSender rmqSender
            , ILogger<TransactionScopeInterceptor> logger
            , IMediator mediator
            , IIndex<Type, IDomainEventFactory> domainEventFactory)
        {
            _logger = logger;
            _dispatchIntegrationEventWhenTransactionCompletedEvent = dispatchIntegrationEventWhenTransactionCompletedEvent;
            _rmqSender = rmqSender;
            _mediator = mediator;
            _domainEventFactory = domainEventFactory;
        }

        public void Intercept(IInvocation invocation)
        {
            using(TransactionScope scope = new TransactionScope())
            {
                invocation.Proceed();

                IDomainEvent domainEvent = _domainEventFactory[invocation.InvocationTarget.GetType()].Generate((IRequest<IModel>)invocation.GetArgumentValue(0), (IModel)invocation.ReturnValue);

                _mediator.Publish(domainEvent);

                Transaction.Current.TransactionCompleted += new TransactionCompletedEventHandler((sender, e) =>
                {
                    _dispatchIntegrationEventWhenTransactionCompletedEvent.Handler(sender, e, _rmqSender, domainEvent);
                });
                scope.Complete();
            }




        }
    }
}
