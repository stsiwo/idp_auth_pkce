using Autofac;
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

            if (currentContext.Equals("OrderingApi"))
            {
                // consumer config (ContextA as consumer)
                builder.RegisterType<OrderingApiConsumer>()
                    .As<IConsumer>()
                    .InstancePerDependency();

                // ContextA consumer needs the other publishers of different contexts
                builder.RegisterType<CatalogApiPublisher>()
                    .As<PublisherBase>()
                    .InstancePerDependency();

                builder.RegisterType<IdentityApiPublisher>()
                    .As<PublisherBase>()
                    .InstancePerDependency();
            }
            else if (currentContext.Equals("CatalogApi"))
            {
                // consumer config (ContextA as consumer)
                builder.RegisterType<CatalogApiConsumer>()
                    .As<IConsumer>()
                    .InstancePerDependency();

                // ContextA consumer needs the other publishers of different contexts
                builder.RegisterType<OrderingApiPublisher>()
                    .As<PublisherBase>()
                    .InstancePerDependency();

                builder.RegisterType<IdentityApiPublisher>()
                    .As<PublisherBase>()
                    .InstancePerDependency();

            }
            else if (currentContext.Equals("IdentityServerAspNetIdentity"))
            {
                // consumer config (ContextA as consumer)
                builder.RegisterType<IdentityApiConsumer>()
                    .As<IConsumer>()
                    .InstancePerDependency();

                // ContextA consumer needs the other publishers of different contexts
                builder.RegisterType<OrderingApiPublisher>()
                    .As<PublisherBase>()
                    .InstancePerDependency();

                builder.RegisterType<CatalogApiPublisher>()
                    .As<PublisherBase>()
                    .InstancePerDependency();
            }


        }
    }
}
