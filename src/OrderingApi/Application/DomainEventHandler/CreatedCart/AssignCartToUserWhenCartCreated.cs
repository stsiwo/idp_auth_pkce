using log4net;
using MediatR;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Application.Repository;
using OrderingApi.Domain.CartAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingApi.Application.DomainEventHandler.CartCreated
{
    public class AssignCartToUserWhenCartCreated : INotificationHandler<CreatedCartDomainEvent>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AssignCartToUserWhenCartCreated));

        private IRepository<Cart> _repository;

        public AssignCartToUserWhenCartCreated(IRepository<Cart> repository)
        {
            _repository = repository;
        }

        public Task Handle(CreatedCartDomainEvent notification, CancellationToken cancellationToken)
        {
            log.Debug("handling assign cart to user when cart created (arrived at local event handler)");

            return Task.CompletedTask;
        }
    }
} 