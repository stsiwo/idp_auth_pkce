using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Base
{
    public class StoredEvent 
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int DomainEventType { get; set; }
        public virtual DateTime OccurredOn { get; set; }
        public virtual string Payload { get; set; }

        public StoredEvent()
        {
        }
    }
}
