using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using Newtonsoft.Json.Linq;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Infrastructure.RabbitMQ.Config.AntiCorruption.Translator;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.AntiCorruption
{
    public class DomainEventAdapter : IDomainEventAdapter
    {
        private readonly IIndex<DomainEventTypeConstants, IDomainEventTranslator> _domainEventTranslatorFactory;

        public DomainEventAdapter(IIndex<DomainEventTypeConstants, IDomainEventTranslator> domainEventTranslatorFactory)
        {
            _domainEventTranslatorFactory = domainEventTranslatorFactory;
        }
        public IDomainEvent Get(DomainEventTypeConstants domainEventTypeConstants, JObject content)
        {
            return _domainEventTranslatorFactory[domainEventTypeConstants].Translate(domainEventTypeConstants, content);
        }
    }
}
