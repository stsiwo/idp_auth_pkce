using MediatR;
using OrderingApi.Application.Command;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Application.DomainEvent.Factory
{
    public interface IDomainEventFactory<in C, in M>
        where C : ICommand
        where M : IModel
    {
        IDomainEvent Generate(C command, M model);
    }
}
