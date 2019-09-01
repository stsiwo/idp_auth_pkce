using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Application.DomainEvent
{
    // #EVENT
    public enum DomainEventTypeConstants
    {
        CartCreatedDomainEvent = 0,
        AddedProductsToCartDomainEvent = 1,
    }
}
