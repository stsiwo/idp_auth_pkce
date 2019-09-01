using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json.Linq;
using OrderingApi.Application.DomainEvent;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.AntiCorruption.Translator
{
    public class CreatedCartDomainEventTranslator : IDomainEventTranslator
    {
        private readonly IMapper _mapper;

        public CreatedCartDomainEventTranslator(IMapper mapper)
        {
            _mapper = mapper;
        }
        public IDomainEvent Translate(DomainEventTypeConstants domainEventTypeConstants, JObject content)
        {
            return _mapper.Map<CreatedCartDomainEvent>(content);

        }

    }
}
