using MediatR;
using OrderingApi.Application.DomainEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace OrderingApi.Application.DomainEventHandler.AddedProductsToCart
{
    public class Test2ndEnlistmentTxScopeWhenAddedProductsToCart : INotificationHandler<AddedProductsToCartDomainEvent>
    {
        public async Task Handle(AddedProductsToCartDomainEvent notification, CancellationToken cancellationToken)
        {
            using(TransactionScope scope = new TransactionScope())
            {
                // do something
                await Task.Delay(1000);

                scope.Complete();
            }
        }
    }
}
