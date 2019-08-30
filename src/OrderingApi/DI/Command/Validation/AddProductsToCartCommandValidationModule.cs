using Autofac;
using FluentValidation;
using OrderingApi.Application.Command;
using OrderingApi.Application.Command.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.Command.Validation
{
    public class AddProductsToCartCommandValidationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AddProductsToCartCommandValidation>()
                .As<IValidator<AddProductsToCartCommand>>()
                .InstancePerLifetimeScope();
        }
    }
}
