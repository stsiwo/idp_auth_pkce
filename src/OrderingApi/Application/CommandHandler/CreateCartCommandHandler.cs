using log4net;
using MediatR;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Infrastructure.MSTransactionScope;
using OrderingApi.Infrastructure.RabbitMQ.Config;
using OrderingApi.Infrastructure.RabbitMQ.Sender;
using OrderingApi.Application.Command;
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
        public CreateCartCommandHandler()
        {
        }

        public Task<int> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(1);
        }
    }
}
