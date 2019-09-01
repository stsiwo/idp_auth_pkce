using OrderingApi.Application.DomainEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config
{
    public class RoutingKeyConstants
    {
        // #CONTEXT
        // need to add new event routing key when new event
        public static string ToCreatedCartDomainEventSubscribers = "api.all.event." + (int)DomainEventTypeConstants.CreatedCartDomainEvent;
        public static string ToAddedProductsToCartDomainEventSubscribers = "api.all.event." + (int)DomainEventTypeConstants.AddedProductsToCartDomainEvent;
        public static string ToAll = "api.*";
        public static string ToCatalogApi = "api.catalog";
        public static string ToOrderingApi = "api.ordering";
        public static string ToIdentityApi = "api.identity";
    }
}
