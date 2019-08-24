using OrderingApi.Application.DomainEvent;
using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Application.Repository
{
    public interface IEventStore
    {
        void Store(IDomainEvent e);
    }
}
