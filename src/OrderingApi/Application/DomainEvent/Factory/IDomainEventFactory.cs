using MediatR;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Application.DomainEvent.Factory
{
    public interface IDomainEventFactory
    {
        IDomainEvent Generate<C, T>(C command, T returnValue)
            where C : IRequest<T>
            where T : IModel;
    }
}
