using Autofac.Features.Indexed;
using Castle.DynamicProxy;
using log4net;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderingApi.Application.Command;
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
    public class TransactionScopeInterceptor : IAsyncInterceptor 
    {
        private readonly ILogger<TransactionScopeInterceptor> _logger;

        private readonly DispatchIntegrationEventWhenTransactionCompletedEvent _dispatchIntegrationEventWhenTransactionCompletedEvent;

        private readonly IRmqSender _rmqSender;

        private readonly IMediator _mediator;

        private readonly DomainEventFactoryWrapper _domainEventFactoryWrapper; 

        public TransactionScopeInterceptor(
             ILogger<TransactionScopeInterceptor> logger
            , DispatchIntegrationEventWhenTransactionCompletedEvent dispatchIntegrationEventWhenTransactionCompletedEvent
            , IRmqSender rmqSender
            , IMediator mediator
            , DomainEventFactoryWrapper domainEventFactoryWrapper)
        {
            _logger = logger;
            _dispatchIntegrationEventWhenTransactionCompletedEvent = dispatchIntegrationEventWhenTransactionCompletedEvent;
            _rmqSender = rmqSender;
            _mediator = mediator;
            _domainEventFactoryWrapper = domainEventFactoryWrapper;
        }
        public void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
        }

        private async Task InternalInterceptAsynchronous(IInvocation invocation)
        {
            // Step 1. Do something prior to invocation.

            invocation.Proceed();
            var task = (Task)invocation.ReturnValue;
            await task;

            // Step 2. Do something after invocation.
        }

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);
        }

        private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
        {
            TResult result;
            // Step 1. Do something prior to invocation.
            _logger.LogDebug("intercepting command handler {0}", invocation.InvocationTarget.GetType().Name);

            using (TransactionScope scope = new TransactionScope())
            {
                _logger.LogDebug("start tx scope ...");

                invocation.Proceed();
                var task = (Task<TResult>)invocation.ReturnValue;
                result = await task;

                // Step 2. Do something after invocation.
                _logger.LogDebug("command object: {0}", JsonConvert.SerializeObject(invocation.GetArgumentValue(0), Formatting.Indented));
                _logger.LogDebug("model (return value) object: {0}", JsonConvert.SerializeObject(result, Formatting.Indented));

                IDomainEvent domainEvent = _domainEventFactoryWrapper.GetFactory()[invocation.InvocationTarget.GetType()]
                    .Generate((ICommand)invocation.GetArgumentValue(0), (IModel)result);

                _logger.LogDebug("publishing domainEvent: {0}", JsonConvert.SerializeObject(domainEvent, Formatting.Indented));

                await _mediator.Publish(domainEvent);

                Transaction.Current.TransactionCompleted += new TransactionCompletedEventHandler((sender, e) =>
                {
                    _logger.LogDebug("transaction scope has completed");
                    _logger.LogDebug("start send integration event to the other context through RABBITMQ");
                    _dispatchIntegrationEventWhenTransactionCompletedEvent.Handler(sender, e, _rmqSender, domainEvent);


                    // #DOUBT : when try to use below, it causes errors. if use above like pass through the class to assign event, it works.
                    //_rmqSender.Send(domainEvent, domainEvent.DomainEventRoutingKey);
                });

                scope.Complete();
            }

            return result;
        }

        public void InterceptSynchronous(IInvocation invocation)
        {
            // Step 1. Do something prior to invocation.

            invocation.Proceed();

            // Step 2. Do something after invocation.
        }
    }
}
