using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Message
{
    public class RmqMessage
    {
        public virtual Guid MessageId { get; set; }

        public virtual int DomainEventType { get; set; }

        public virtual string Sender { get; set; }

        public virtual DateTime OccurredOn { get; set; }

        public virtual JObject Content { get; set; }

        public virtual int DeliveryTag { get; set; }

        public virtual MessageStatusConstants Status { get; set; }

        public RmqMessage()
        {

        }

    }
}
