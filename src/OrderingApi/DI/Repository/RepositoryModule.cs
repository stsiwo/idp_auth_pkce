using Autofac;
using Autofac.Extras.DynamicProxy;
using OrderingApi.Application.Repository;
using OrderingApi.Config.AOP;
using OrderingApi.Domain.Base;
using OrderingApi.Domain.CartAgg;
using OrderingApi.Domain.OrderAgg;
using OrderingApi.Domain.UserAgg;
using OrderingApi.Infrastructure.Repository;
using OrderingApi.Infrastructure.Repository.MessageStorage.Consuming;
using OrderingApi.Infrastructure.Repository.MessageStorage.Publishing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.Repository
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CartRepository>()
                .As<IRepository<Cart>>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerLifetimeScope();

            builder.RegisterType<OrderRepository>()
                .As<IRepository<Order>>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerLifetimeScope();

            builder.RegisterType<UserRepository>()
                .As<IRepository<User>>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerLifetimeScope();

            builder.RegisterType<EventStore>()
                .As<IEventStore>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerLifetimeScope();

            builder.RegisterType<PublishedMessageStore>()
                .As<IPublishedMessageStore>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerLifetimeScope();

            builder.RegisterType<ConsumedMessageStore>()
                .As<IConsumedMessageStore>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerLifetimeScope();
        }
    }
}
