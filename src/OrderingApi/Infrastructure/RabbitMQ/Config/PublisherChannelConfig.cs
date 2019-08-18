using Autofac.Features.Indexed;
using Autofac.Features.Metadata;
using OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config
{
    public class PublisherChannelConfig
    {
        private ICurrentPublisher _publisher;

        private IConnection _publisherConnection;

        public PublisherChannelConfig(ICurrentPublisher publisher, IIndex<ConnectionTypeConstants, IConnection> connectionFactory)
        {
            _publisher = publisher; 
            _publisherConnection = connectionFactory[ConnectionTypeConstants.Publisher];
        }
        public IModel Configure()
        {
            // create channel
            var channel = _publisherConnection.CreateModel();

            // configure this context as publisher
            _publisher.Configure(channel);

            return channel;
        }

    }
}
