using log4net;
using MediatR;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Infrastructure.MSTransactionScope;
using OrderingApi.Infrastructure.RabbitMQ.Config;
using OrderingApi.Infrastructure.RabbitMQ.Sender;
using OrderingApi.Application.Command;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using OrderingApi.Domain.CartAgg;
using OrderingApi.Application.Repository;

namespace OrderingApi.Application.CommandHandler
{
    public class AddProductsToCartCommandHandler : IRequestHandler<AddProductsToCartCommand, CartModel>
    {
        private readonly IRepository<Cart> _cartRepository;

        public IDomainEvent _domainEvent;

        public AddProductsToCartCommandHandler(IRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<CartModel> Handle(AddProductsToCartCommand request, CancellationToken cancellationToken)
        {
            // DON'T foreget set domainevent. this is dispatched AOP interceptor after complete transaction.
            return new CartModel()
            {
                Id = Guid.NewGuid(),
            };
        }
    }
}
