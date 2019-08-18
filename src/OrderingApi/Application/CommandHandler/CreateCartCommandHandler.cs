using log4net;
using MediatR;
using OrderingApi.Application.DomainEvent;
using OrderingApi.UI.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingApi.Application.CommandHandler
{
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, int>
    {
        private readonly IMediator _mediator;

        private static readonly ILog log = LogManager.GetLogger(typeof(CreateCartCommandHandler));

        public CreateCartCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<int> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {

            log.Debug("start handling command ...");

            log.Debug("start dispatch event...");
            _mediator.Publish(new CartCreatedDomainEvent("test-dispatch"));

            return Task.FromResult(1);
        }
    }
}
