using log4net;
using MediatR;
using OrderingApi.Infrastructure.RabbitMQ.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Application.DomainEvent
{
    public class AddedProductsToCartDomainEvent : DomainEventBase, INotification 
    {
        public Guid CartId { get; set; }

        public IList<Guid> ProductIds { get; set; } 

        public AddedProductsToCartDomainEvent()
        {

        }
        public AddedProductsToCartDomainEvent(Guid cartId, IList<Guid> productIds)
        {
            CartId = cartId;
            ProductIds = productIds;
            DomainEventRoutingKey = RoutingKeyConstants.ToAddedProductsToCartDomainEventSubscribers;
        }
    }
}
