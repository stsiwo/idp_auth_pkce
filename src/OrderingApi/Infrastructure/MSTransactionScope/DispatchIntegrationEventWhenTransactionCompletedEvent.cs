using MediatR;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Infrastructure.RabbitMQ.Sender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace OrderingApi.Infrastructure.MSTransactionScope
{
    public class DispatchIntegrationEventWhenTransactionCompletedEvent
    {
        public void Handler(object sender, TransactionEventArgs e, IRmqSender rmqSender, IDomainEvent domainEvent)
        {
            // send this domain event to messaging bus (rabbitmq)
           rmqSender.Send(domainEvent, domainEvent.DomainEventRoutingKey); 
        }
    }
}
