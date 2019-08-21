using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher
{
    // #CONTEXT
    public class PublisherBase : IPublisher
    {
        public string ExchangeName { get; }

        public string RoutingKey { get; }

        public PublisherBase(string exchangeName, string routingKey)
        {
            ExchangeName = exchangeName;
            RoutingKey = routingKey;
        }
        public void Configure(IModel channel)
        {
            EnablePublisherConfirm(channel);

            SubscribePublisherConfirmReceivedFromBrokerEvent(channel);

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

        private void SubscribePublisherConfirmReceivedFromBrokerEvent(IModel channel)
        {
        }

        // also need to impl re-send message when broker return nack to this publisher client
        // #REFACTOR
    }
}
