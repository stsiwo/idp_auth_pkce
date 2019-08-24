using log4net;
using MediatR;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Infrastructure.MSTransactionScope;
using OrderingApi.Infrastructure.RabbitMQ.Config;
using OrderingApi.Infrastructure.RabbitMQ.Sender;
using OrderingApi.UI.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace OrderingApi.Application.CommandHandler
{
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, int>
    {
        private readonly IMediator _mediator;

        private static readonly ILog log = LogManager.GetLogger(typeof(CreateCartCommandHandler));

        private readonly DispatchIntegrationEventWhenTransactionCompletedEvent _dispatchIntegrationEventWhenTransactionCompletedEvent;

        private readonly IRmqSender _rmqSender;

        public CreateCartCommandHandler(IMediator mediator, DispatchIntegrationEventWhenTransactionCompletedEvent dispatchIntegrationEventWhenTransactionCompletedEvent, IRmqSender rmqSender)
        {
            _mediator = mediator;
            _dispatchIntegrationEventWhenTransactionCompletedEvent = dispatchIntegrationEventWhenTransactionCompletedEvent;
            _rmqSender = rmqSender;
        }

        public Task<int> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            using(TransactionScope scope = new TransactionScope())
            {
                log.Debug("start handling command ...");

                log.Debug("start dispatch event...");

                IDomainEvent domainEvent = new CartCreatedDomainEvent()
                {
                    DomainEventId = Guid.NewGuid().ToString(),
                    DomainEventName = "CartCreated",
                    DomainEventRoutingKey = RoutingKeyConstants.ToCartCreatedDomainEventSubscribers,
                    DomainEventType = 0,
                    CartId = "test-cart-id"
                };

                _mediator.Publish(domainEvent);

                Transaction.Current.TransactionCompleted += new TransactionCompletedEventHandler((sender, e) =>
                {
                    _dispatchIntegrationEventWhenTransactionCompletedEvent.Handler(sender, e, _rmqSender, domainEvent);
                });
                scope.Complete();
            }
            return Task.FromResult(1);
        }
    }
}
