using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher
{
    public class CatalogApiPublisher : PublisherBase
    {
        public CatalogApiPublisher() : base(ExchangeNameConstants.CatalogApiPublisherExchange, RoutingKeyConstants.ToCatalogApi)
        {
        }
    }
}
