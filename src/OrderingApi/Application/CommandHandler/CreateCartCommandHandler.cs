using MediatR;
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
        public Task<int> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(1);
        }
    }
}
