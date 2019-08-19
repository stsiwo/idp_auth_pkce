using Autofac;
using OrderingApi.Infrastructure.Repository.StoredEventTranslator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.Repository
{
    public class StoredEventTranslatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StoredEventTranslatorBase>()
                .As<IStoredEventTranslator>()
                .InstancePerLifetimeScope();
        }
    }
}
