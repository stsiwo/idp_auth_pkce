using Autofac.Features.Indexed;
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
        private IPublisher _contextAPublisher;

        private IConnection _publisherConnection;

        public PublisherChannelConfig(IPublisher contextAPublisher, IIndex<ConnectionTypeConstants, IConnection> connectionFactory)
        {
            _contextAPublisher = contextAPublisher;
            _publisherConnection = connectionFactory[ConnectionTypeConstants.Publisher];
        }
        public IModel Configure()
        {
            // create channel
            var channel = _publisherConnection.CreateModel();

            // configure this context as publisher
            _contextAPublisher.Configure(channel);

            return channel;
        }

    }
}
