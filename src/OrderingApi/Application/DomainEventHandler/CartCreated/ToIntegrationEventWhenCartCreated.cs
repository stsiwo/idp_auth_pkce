using log4net;
using MediatR;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Infrastructure.RabbitMQ.Config;
using OrderingApi.Infrastructure.RabbitMQ.Sender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingApi.Application.DomainEventHandler.CartCreated
{
    public class ToIntegrationEventWhenCartCreated : INotificationHandler<CartCreatedDomainEvent>
    {
        private IRmqSender _rmqSender;

        private static readonly ILog log = LogManager.GetLogger(typeof(ToIntegrationEventWhenCartCreated));

        public ToIntegrationEventWhenCartCreated(IRmqSender rmqSender)
        {
            _rmqSender = rmqSender;
        }
        public Task Handle(CartCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            log.Debug("received event at local integration event handler and start sending this event as message using RabbitMQ");

            // send this domain event to messaging bus (rabbitmq)
            _rmqSender.Send(notification, RoutingKeyConstants.ToCartCreatedDomainEventSubscribers);

            return Task.CompletedTask;
        }
    }
}
