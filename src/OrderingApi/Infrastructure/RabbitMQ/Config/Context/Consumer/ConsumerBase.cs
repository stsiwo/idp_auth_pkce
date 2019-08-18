using Autofac.Features.Metadata;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.Context.Consumer
{
    public class ConsumerBase : IConsumer
    {
        private IEnumerable<IPublisher> _publishers;

        private string _queueName;

        private static Array _eventIds = Enum.GetValues(typeof(DomainEventTypeConstants));

        public ConsumerBase(IEnumerable<IPublisher> publishers, string queueName)
        {
            _publishers = publishers;
            _queueName = queueName;
        }
        public void Configure(IModel channel)
        {
            // declare a queue for this consumer 
            channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false);

            foreach (IPublisher publisher in _publishers)
            {
                // declare each publishder's exchange on this consumer channel
                publisher.DeclareExchangeIn(channel);

                // bind the exchange to the queue with routingKey for point-to-point delivery
                channel.QueueBind(queue: _queueName,
                                  exchange: publisher.ExchangeName,
                                  routingKey: publisher.RoutingKey);
                // bind this excahnge to the queue with routing key all; when message's RK is "all" => the message must be delivered to this queue 
                channel.QueueBind(queue: _queueName,
                                  exchange: publisher.ExchangeName,
                                  routingKey: RoutingKeyConstants.ToAll);

                // bind this exchange to the queue with routing key of all events
                foreach (DomainEventTypeConstants eventId in _eventIds)
                {
                    channel.QueueBind(queue: _queueName,
                                      exchange: publisher.ExchangeName,
                                      routingKey: "api.*.event." + (int)eventId);
                }

            }
        }

        public string GetQueueName()
        {
            return _queueName;
        }
    }
}
