using NHibernate;
using OrderingApi.Infrastructure.RabbitMQ.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.Repository
{
    public interface IPublishedMessageStore
    {
        RmqMessage GetByMessageId(Guid id);

        RmqMessage GetByDeliveryTag(ulong deliveryTag);

        Guid Create(RmqMessage message);

        void Update(RmqMessage updatedMessage);

        ITransaction BeginTransaction();

        void Commit(ITransaction transaction);

        void Rollback(ITransaction transaction);
    }
}
