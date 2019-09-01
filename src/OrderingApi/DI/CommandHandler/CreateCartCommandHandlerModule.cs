using Autofac;
using Autofac.Extras.DynamicProxy;
using MediatR;
using OrderingApi.Application.CommandHandler;
using OrderingApi.Config.AOP;
using OrderingApi.Application.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.CommandHandler
{
    public class CreateCartCommandHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreateCartCommandHandler>()
                .As<IRequestHandler<CreateCartCommand, int>>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LoggingInterceptor))
                .InterceptedBy(typeof(TransactionScopeInterceptor))
                .InstancePerDependency();
        }
    }
}
