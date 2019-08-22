using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;
using OrderingApi.Infrastructure.RabbitMQ.Message;

namespace OrderingApi.Infrastructure.Repository
{
    public class MessageStore : IMessageStore
    {
        protected ISession _session;

        public MessageStore(ISession session)
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

        public Guid Create(RmqMessage message)
        {
            return (Guid)_session.Save(message);
        }

        public RmqMessage GetByDeliveryTag(ulong deliveryTag)
        {
            return (RmqMessage)_session.CreateCriteria<RmqMessage>()
                .Add(Expression.Eq("DeliveryTag", deliveryTag))
                .UniqueResult();
        }

        public RmqMessage GetByMessageId(Guid id)
        {
            return (RmqMessage)_session.Get<RmqMessage>(id);
        }

        public void Rollback(ITransaction transaction)
        {
            transaction.Rollback();
        }

        public void Update(RmqMessage updatedMessage)
        {
            _session.Update(updatedMessage);
        }
    }
}
