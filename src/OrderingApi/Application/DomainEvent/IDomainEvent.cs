using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Application.DomainEvent
{
    public interface IDomainEvent
    {
        string DomainEventId { get;  }
        string DomainEventName { get;  }
        int DomainEventType { get;  }

        DateTime OccurredOn { get; }

        string DomainEventRoutingKey { get; }
    }
}
