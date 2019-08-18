using MediatR;
using OrderingApi.Application.DomainEvent;
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

        public ToIntegrationEventWhenCartCreated(IRmqSender rmqSender)
        {
            _rmqSender = rmqSender;
        }
        public Task Handle(CartCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            // send this domain event to messaging bus (rabbitmq)
            _rmqSender.Send(notification);

            return Task.CompletedTask;
        }
    }
}
