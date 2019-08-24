using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Message
{
    public interface IRmqMessage
    {
        Guid MessageId { get; set; }
        int Version { get; set; }

        int DomainEventType { get; set; }

        string Sender { get; set; }

        DateTime OccurredOn { get; set; }

        JObject Content { get; set; }

        ulong DeliveryTag { get; set; }

        MessageStatusConstants Status { get; set; }
        string StatusReason { get; set; }
    }
}
