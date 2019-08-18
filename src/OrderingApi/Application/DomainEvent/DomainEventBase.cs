using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Application.DomainEvent
{
    public abstract class DomainEventBase : IDomainEvent
    {
        public string DomainEventId { get; set; }

        public int DomainEventType { get; set; }

        public int GetDomainEventType()
        {
            return DomainEventType;
        }
    }
}
