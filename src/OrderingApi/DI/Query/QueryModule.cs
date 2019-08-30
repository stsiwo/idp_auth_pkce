using Autofac;
using OrderingApi.Application.Query;
using OrderingApi.Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.Query
{
    public class QueryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CartQuery>()
                .As<ICartQuery>()
                .InstancePerLifetimeScope();
        }
    }
}
