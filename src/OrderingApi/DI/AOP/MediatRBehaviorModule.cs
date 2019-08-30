using Autofac;
using MediatR;
using OrderingApi.Application.Command;
using OrderingApi.Config.AOP.MediatRBehaviors;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.AOP
{
    public class MediatRBehaviorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // autofac polymorphic dispatching does not work 
            // builder.RegisterGeneric(typeof(BehaviorOne<,>)).As(typeof(IPipelineBehavior<,>));
            // when have a time, find workaournd: https://github.com/jbogard/MediatR/issues/128

            // for now you need to add new one when new validation added
            builder.RegisterType<CommandValidatorBehavior<AddProductsToCartCommand, CartModel>>()
                .As<IPipelineBehavior<AddProductsToCartCommand, CartModel>>()
                .SingleInstance();
        }
    }
}
