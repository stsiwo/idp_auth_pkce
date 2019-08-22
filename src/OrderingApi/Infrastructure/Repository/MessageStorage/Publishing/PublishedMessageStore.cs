using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;
using OrderingApi.Infrastructure.RabbitMQ.Message;

namespace OrderingApi.Infrastructure.Repository.MessageStorage.Publishing
{
    public class PublishedMessageStore : IPublishedMessageStore
    {
        protected ISession _session;

        public PublishedMessageStore(ISession session)
        {
            _session = session;
        }

        public ITransaction BeginTransaction()
        {
            return _session.BeginTransaction();
        }

        public void Commit(ITransaction transaction)
        {
            transaction.Commit();
        }

        public Guid Create(RmqPublishMessage message)
        {
            return (Guid)_session.Save(message);
        }

        public RmqPublishMessage GetByDeliveryTag(ulong deliveryTag)
        {
            return (RmqPublishMessage)_session.CreateCriteria<RmqPublishMessage>()
                .Add(Expression.Eq("DeliveryTag", deliveryTag))
                .UniqueResult();
        }

        public RmqPublishMessage GetByMessageId(Guid id)
        {
            return (RmqPublishMessage)_session.Get<RmqPublishMessage>(id);
        }

        public void Rollback(ITransaction transaction)
        {
            transaction.Rollback();
        }

        public void Update(RmqPublishMessage updatedMessage)
        {
            _session.Update(updatedMessage);
        }
    }
}
