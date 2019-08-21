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
        public RmqMessage Create(RmqMessage message)
        {
            return (RmqMessage)_session.Save(message);
        }

        public RmqMessage GetByDeliveryTag(int deliveryTag)
        {
            return (RmqMessage)_session.CreateCriteria<RmqMessage>()
                .Add(Expression.Eq("DeliveryTag", deliveryTag))
                .UniqueResult();
        }

        public RmqMessage GetByMessageId(Guid id)
        {
            return (RmqMessage)_session.Get<RmqMessage>(id);
        }

        public void Update(RmqMessage updatedMessage)
        {
            _session.Update(updatedMessage);
            //  don't need call flush() to apply the change for persistent
            // because tx.commit takes this responsibility of that. 
        }
    }
}
