using Autofac;
using OrderingApi.Infrastructure.MSTransactionScope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.MSTransactionScope
{
    public class TransactionEventHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DispatchIntegrationEventWhenTransactionCompletedEvent>()
                .InstancePerDependency();
        }
    }
}
