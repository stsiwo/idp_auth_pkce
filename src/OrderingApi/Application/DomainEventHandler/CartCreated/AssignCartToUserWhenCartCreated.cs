using log4net;
using MediatR;
using OrderingApi.Application.DomainEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingApi.Application.DomainEventHandler.CartCreated
{
    public class AssignCartToUserWhenCartCreated : INotificationHandler<CartCreatedDomainEvent>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AssignCartToUserWhenCartCreated));

        public Task Handle(CartCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            log.Debug("handling assign cart to user when cart created");

            return Task.CompletedTask;
        }
    }
}
