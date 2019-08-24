using Autofac;
using OrderingApi.Infrastructure.RabbitMQ.Config;
using OrderingApi.Infrastructure.RabbitMQ.Config.Context.Consumer;
using OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.RabbitMQ
{
    public class ConsumerConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var currentContext = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

            // #CONTEXT
            if (currentContext.Equals("OrderingApi"))
            {
                // consumer config (ContextA as consumer)
                builder.RegisterType<OrderingApiConsumer>()
                    .As<IConsumer>()
                    .SingleInstance();

                // ContextA consumer needs the other publishers of different contexts
                builder.RegisterType<CatalogApiPublisher>()
                    .As<IPublisher>()
                    .SingleInstance();

                builder.RegisterType<IdentityApiPublisher>()
                    .As<IPublisher>()
                    .SingleInstance();
            }
            else if (currentContext.Equals("CatalogApi"))
            {
                // consumer config (ContextA as consumer)
                builder.RegisterType<CatalogApiConsumer>()
                    .As<IConsumer>()
                    .SingleInstance();

                // ContextA consumer needs the other publishers of different contexts
                builder.RegisterType<OrderingApiPublisher>()
                    .As<IPublisher>()
                    .SingleInstance();

                builder.RegisterType<IdentityApiPublisher>()
                    .As<IPublisher>()
                    .SingleInstance();

            }
            else if (currentContext.Equals("IdentityServerAspNetIdentity"))
            {
                // consumer config (ContextA as consumer)
                builder.RegisterType<IdentityApiConsumer>()
                    .As<IConsumer>()
                    .SingleInstance();

                // ContextA consumer needs the other publishers of different contexts
                builder.RegisterType<OrderingApiPublisher>()
                    .As<IPublisher>()
                    .SingleInstance();

                builder.RegisterType<CatalogApiPublisher>()
                    .As<IPublisher>()
                    .SingleInstance();
            }


        }
    }
}
