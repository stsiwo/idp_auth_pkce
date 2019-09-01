using MediatR;
using OrderingApi.Application.Command;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Application.DomainEvent.Factory
{
    public class CreatedCartDomainEventFactory : IDomainEventFactory<ICommand, IModel>
    {
        public IDomainEvent Generate(ICommand command, IModel model)
        {
            var commandInContext = (CreateCartCommand)command;
            var modelInContext = model;

            return new CreatedCartDomainEvent();
        }
    }
}
