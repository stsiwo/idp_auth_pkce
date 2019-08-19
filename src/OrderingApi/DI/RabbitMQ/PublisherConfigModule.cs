using Autofac;
using OrderingApi.Infrastructure.RabbitMQ.Config;
using OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.RabbitMQ
{
    public class PublisherConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var currentContext = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

            // #CONTEXT
            // publisher config (ContextA as publisher)
            if (currentContext.Equals("OrderingApi"))
            {
                builder.RegisterType<OrderingApiPublisher>()
                    .As<ICurrentPublisher>()
                    .SingleInstance();
            } 
            else if (currentContext.Equals("CatalogApi"))
            {
                builder.RegisterType<CatalogApiPublisher>()
                    .As<ICurrentPublisher>()
                    .SingleInstance();
            }
            else if (currentContext.Equals("IdentityServerAspNetIdentity"))
            {
                builder.RegisterType<IdentityApiPublisher>()
                    .As<ICurrentPublisher>()
                    .SingleInstance();
            }


        }
    }
}
