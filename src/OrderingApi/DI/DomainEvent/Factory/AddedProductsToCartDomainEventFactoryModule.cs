using Autofac;
using Autofac.Features.Variance;
using OrderingApi.Application.Command;
using OrderingApi.Application.CommandHandler;
using OrderingApi.Application.DomainEvent.Factory;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.DomainEvent.Factory
{
    public class AddedProductsToCartDomainEventFactoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AddedProductsToCartDomainEventFactory>()
                .Keyed<IDomainEventFactory<ICommand, IModel>>(typeof(AddProductsToCartCommandHandler))
                .InstancePerDependency(); 
        }
    }
}
