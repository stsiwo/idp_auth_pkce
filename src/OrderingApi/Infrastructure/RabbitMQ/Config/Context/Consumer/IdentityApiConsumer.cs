using Autofac.Features.Metadata;
using OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.Context.Consumer
{
    public class IdentityApiConsumer : ConsumerBase
    {
        public IdentityApiConsumer(IEnumerable<IPublisher> publishers) : base(publishers, QueueNameConstants.IdentityApiConsumerQueue)
        {
        }
    }
}
