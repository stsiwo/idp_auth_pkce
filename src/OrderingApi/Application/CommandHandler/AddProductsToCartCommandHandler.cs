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

namespace OrderingApi.Application.CommandHandler
{
    public class AddProductsToCartCommandHandler : IRequestHandler<AddProductsToCartCommand, CartModel>
    {
        public async Task<CartModel> Handle(AddProductsToCartCommand request, CancellationToken cancellationToken)
        {
            return new CartModel()
            {
                Id = "you did it!!!"
            };
        }
    }
}
