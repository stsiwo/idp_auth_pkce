using log4net;
using MediatR;
using OrderingApi.Infrastructure.RabbitMQ.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Application.DomainEvent
{
    public class CreatedCartDomainEvent : DomainEventBase, INotification 
    {
        public string CartId { get; set; }

        public CreatedCartDomainEvent()
        {

        }
        public CreatedCartDomainEvent(string cartId)
        {
            CartId = cartId;
            DomainEventRoutingKey = RoutingKeyConstants.ToCreatedCartDomainEventSubscribers;
        }
    }
}
