using OrderingApi.Infrastructure.RabbitMQ.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.Repository.MessageStorage
{
    public interface IMessageStore<T>
        where T : IRmqMessage
    {
        T GetByMessageId(Guid id);
        Guid Create(T message);

        void Update(T updatedMessage);
    }
}
