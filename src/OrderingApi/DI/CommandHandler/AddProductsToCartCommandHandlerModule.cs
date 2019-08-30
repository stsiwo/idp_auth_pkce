using Autofac;
using Autofac.Extras.DynamicProxy;
using MediatR;
using OrderingApi.Application.CommandHandler;
using OrderingApi.Config.AOP;
using OrderingApi.Application.Command;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.CommandHandler
{
    public class AddProductsToCartCommandHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AddProductsToCartCommandHandler>()
                .As<IRequestHandler<AddProductsToCartCommand, CartModel>>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerDependency();
        }
    }
}
