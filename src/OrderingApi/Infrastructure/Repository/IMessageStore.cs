using OrderingApi.Infrastructure.RabbitMQ.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.Repository
{
    public interface IMessageStore
    {
        RmqMessage GetByMessageId(Guid id);

        RmqMessage GetByDeliveryTag(int deliveryTag);

        RmqMessage Create(RmqMessage message);

        void Update(RmqMessage updatedMessage);

    }
}
