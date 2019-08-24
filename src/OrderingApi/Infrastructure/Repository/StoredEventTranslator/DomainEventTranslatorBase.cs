using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Domain.Base;

namespace OrderingApi.Infrastructure.Repository.StoredEventTranslator
{
    public class StoredEventTranslatorBase : IStoredEventTranslator
    {
        private IMapper _mapper;

        public StoredEventTranslatorBase(IMapper mapper)
        {
            _mapper = mapper;
        }

        public StoredEvent TranslateToStoredEvent(IDomainEvent domainEvent)
        {
            return _mapper.Map<StoredEvent>(domainEvent); 
        }
    }
}
