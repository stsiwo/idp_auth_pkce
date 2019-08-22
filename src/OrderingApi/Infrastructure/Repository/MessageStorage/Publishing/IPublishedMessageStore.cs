using NHibernate;
using OrderingApi.Infrastructure.RabbitMQ.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.Repository.MessageStorage.Publishing
{
    public interface IPublishedMessageStore : IMessageStore<RmqPublishMessage>
    {
        RmqPublishMessage GetByDeliveryTag(ulong deliveryTag);

        ITransaction BeginTransaction();

        void Commit(ITransaction transaction);

        void Rollback(ITransaction transaction);
    }
}
