using Autofac.Features.Indexed;
using OrderingApi.Application.Command;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Application.DomainEvent.Factory
{
    /**
     * use this because to inject IIndex implicitly
     *  - autofac does not allow to resolve IIndex explicitly (e.g, using Resolve function)
     **/
    public class DomainEventFactoryWrapper
    {
        private readonly IIndex<Type, IDomainEventFactory<ICommand, IModel>> _domainEventFactory;

        public DomainEventFactoryWrapper(IIndex<Type, IDomainEventFactory<ICommand, IModel>> domainEventFactory)
        {
            _domainEventFactory = domainEventFactory;
        }

        public IIndex<Type, IDomainEventFactory<ICommand, IModel>> GetFactory()
        {
            return _domainEventFactory;
        }
    }
}
