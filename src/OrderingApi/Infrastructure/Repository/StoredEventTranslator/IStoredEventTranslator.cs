using OrderingApi.Application.DomainEvent;
using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.Repository.StoredEventTranslator
{
    // translate IDomainEvent to StoredEvent and vice versa
    public interface IStoredEventTranslator
    {
        StoredEvent TranslateToStoredEvent(IDomainEvent domainEvent);
    }
}
