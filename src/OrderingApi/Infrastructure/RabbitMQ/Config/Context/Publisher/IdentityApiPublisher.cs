using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher
{
    public class IdentityApiPublisher : PublisherBase
    {
        public IdentityApiPublisher() : base(ExchangeNameConstants.IdentityApiPublisherExchange, RoutingKeyConstants.ToIdentityApi)
        {
        }
    }
}
