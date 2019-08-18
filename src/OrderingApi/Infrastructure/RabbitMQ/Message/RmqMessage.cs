using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Message
{
    public class RmqMessage
    {
        public string MessageId { get; set; }

        public int DomainEventType { get; set; }

        public JObject Content { get; set; }

    }
}
