using AutoMapper;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Application.Repository;
using OrderingApi.Domain.Base;
using OrderingApi.Infrastructure.Repository.StoredEventTranslator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderingApi.Infrastructure.Repository
{
    public class EventStore : IEventStore 
    {
        // stored as StoredEvent object for the sake of payload
        private ISession _session;

        private IStoredEventTranslator _domainEventTranslator;

        public EventStore(ISession session, IStoredEventTranslator domainEventTranslator)
        {
            _session = session;
            _domainEventTranslator = domainEventTranslator;
        }

        public void Store(IDomainEvent e)
        {
            _session.Save(_domainEventTranslator.TranslateToStoredEvent(e));
        }
    }
}
