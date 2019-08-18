using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config
{
    public class ExchangeNameConstants
    {
        // add new enum when new exchange
        public static string CatalogApiPublisherExchange = "catalog-api-publisher-exchange";
        public static string OrderingApiPublisherExchange = "ordering-api-publisher-exchange";
        public static string IdentityApiPublisherExchange = "identity-api-publisher-exchange";

    }
}
