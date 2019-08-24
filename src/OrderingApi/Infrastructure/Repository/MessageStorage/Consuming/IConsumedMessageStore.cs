using NHibernate;
using OrderingApi.Infrastructure.RabbitMQ.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.Repository.MessageStorage.Consuming
{
    public interface IConsumedMessageStore : IMessageStore<RmqConsumeMessage>
    {
    }
}
