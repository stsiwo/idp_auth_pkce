using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;
using OrderingApi.Infrastructure.RabbitMQ.Message;

namespace OrderingApi.Infrastructure.Repository.MessageStorage.Consuming
{
    public class ConsumedMessageStore : IConsumedMessageStore
    {
        protected ISession _session;

        public ConsumedMessageStore(ISession session)
        {
            _session = session;
        }

        public Guid Create(RmqConsumeMessage message)
        {
            return (Guid)_session.Save(message);
        }

        public RmqConsumeMessage GetByMessageId(Guid id)
        {
            return (RmqConsumeMessage)_session.Get<RmqConsumeMessage>(id);
        }

        public void Update(RmqConsumeMessage updatedMessage)
        {
            _session.Update(updatedMessage);
        }
    }
}
