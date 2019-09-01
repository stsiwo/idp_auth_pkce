using MediatR;
using OrderingApi.Application.Command;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Application.DomainEvent.Factory
{
    public class AddedProductsToCartDomainEventFactory : IDomainEventFactory
    {
        public IDomainEvent Generate(AddProductsToCartCommand command, CartModel returnValue)
        {
            return new AddedProductsToCartDomainEvent(returnValue.Id, command.ProductIds);
        }

        public IDomainEvent Generate<C, T>(C command, T returnValue)
            where C : IRequest<T>
            where T : IModel
        {
            throw new NotImplementedException();
        }
    }
}
