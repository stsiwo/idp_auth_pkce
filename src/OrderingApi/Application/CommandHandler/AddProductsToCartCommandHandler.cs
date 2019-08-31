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

        private readonly IMediator _mediator;

        public AddProductsToCartCommandHandler(IRepository<Cart> cartRepository, IMediator mediator)
        {
            _cartRepository = cartRepository;
            _mediator = mediator;
        }
        public async Task<CartModel> Handle(AddProductsToCartCommand request, CancellationToken cancellationToken)
        {

        }
    }
}
