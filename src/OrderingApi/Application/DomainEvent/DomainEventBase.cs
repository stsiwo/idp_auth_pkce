using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Application.DomainEvent
{
    // #EVENT
    public abstract class DomainEventBase : IDomainEvent
    {
        public string DomainEventId { get; set; }

        public int DomainEventType { get; set; }

        public string DomainEventName { get; set; } 

        public DateTime OccurredOn { get; set; }

        public string DomainEventRoutingKey { get; set; } 
    }
}
