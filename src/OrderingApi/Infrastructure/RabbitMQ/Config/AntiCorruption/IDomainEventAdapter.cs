using Newtonsoft.Json.Linq;
using OrderingApi.Application.DomainEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.AntiCorruption
{
    public interface IDomainEventAdapter
    {
        IDomainEvent Get(DomainEventTypeConstants domainEventTypeConstants, JObject content);

    }
}
