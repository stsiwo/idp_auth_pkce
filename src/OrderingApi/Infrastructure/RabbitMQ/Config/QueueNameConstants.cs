using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config
{
    public class QueueNameConstants
    {
        // #CONTEXT
        // add enum when new queue
        public static string CatalogApiConsumerQueue = "catalog-api-queue";
        public static string OrderingApiConsumerQueue = "ordering-api-queue";
        public static string IdentityApiConsumerQueue = "identity-api-queue";
    }
}
