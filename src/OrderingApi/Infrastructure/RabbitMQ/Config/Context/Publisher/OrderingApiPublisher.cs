using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher
{
    public class OrderingApiPublisher : PublisherBase
    {
        public OrderingApiPublisher() : base(ExchangeNameConstants.OrderingApiPublisherExchange, RoutingKeyConstants.ToOrderingApi)
        {
        }
    }
}
