using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Application.DomainEvent
{
    public interface IDomainEvent
    {
        int GetDomainEventType();
    }
}
