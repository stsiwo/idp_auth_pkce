using Autofac;
using OrderingApi.Application.DomainEvent.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.DomainEvent.Factory
{
    public class DomainEventFactoryWrapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DomainEventFactoryWrapper>()
                .InstancePerDependency();
        }
    }
}
