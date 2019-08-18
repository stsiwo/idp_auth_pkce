using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher
{
    public class PublisherBase : IPublisher
    {
        public string ExchangeName { get; private set; }

        public string RoutingKey { get; private set; }

        public PublisherBase(string exchangeName, string routingKey)
        {
            ExchangeName = exchangeName;
            RoutingKey = routingKey;
        }
        public void Configure(IModel channel)
        {
            EnablePublisherConfirm(channel);

            DeclareExchangeIn(channel);
        }

        public void DeclareExchangeIn(IModel channel)
        {
            channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeTypeConstants.Topic);
        }

        private void EnablePublisherConfirm(IModel channel)
        {
            channel.ConfirmSelect();
        }
    }
}
