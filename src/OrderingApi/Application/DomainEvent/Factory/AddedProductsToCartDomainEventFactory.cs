﻿using MediatR;
using OrderingApi.Application.Command;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Application.DomainEvent.Factory
{
    public class AddedProductsToCartDomainEventFactory : IDomainEventFactory<ICommand, IModel>
    {
        public IDomainEvent Generate(ICommand command, IModel model)
        {
            var commandInContext = (AddProductsToCartCommand)command;
            var modelInContext = (CartModel)model;

            return new AddedProductsToCartDomainEvent(cartId: modelInContext.Id, productIds: commandInContext.ProductIds);
        }
    }
}
