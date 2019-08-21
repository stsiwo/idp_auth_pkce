using Autofac;
using OrderingApi.Application.Repository;
using OrderingApi.Domain.Base;
using OrderingApi.Domain.CartAgg;
using OrderingApi.Domain.OrderAgg;
using OrderingApi.Domain.UserAgg;
using OrderingApi.Infrastructure.Repository;
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
                .InstancePerLifetimeScope();

            builder.RegisterType<OrderRepository>()
                .As<IRepository<Order>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserRepository>()
                .As<IRepository<User>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EventStore>()
                .As<IEventStore>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MessageStore>()
                .As<IMessageStore>()
                .InstancePerLifetimeScope();
        }
    }
}
