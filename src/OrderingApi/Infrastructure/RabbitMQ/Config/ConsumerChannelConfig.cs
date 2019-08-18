using Autofac.Features.Indexed;
using MediatR;
using Newtonsoft.Json.Linq;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Infrastructure.RabbitMQ.Config.AntiCorruption;
using OrderingApi.Infrastructure.RabbitMQ.Config.Context.Consumer;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config
{
    public class ConsumerChannelConfig
    {
        private IConnection _consumerConnection;

        private IDomainEventAdapter _domainEventFactory;

        private IMediator _mediator;

        private IConsumer _consumer;

        public ConsumerChannelConfig(IIndex<ConnectionTypeConstants, IConnection> connectionFactory, IDomainEventAdapter domainEventFactory, IMediator mediator, IConsumer Consumer)
        {
            _consumerConnection = connectionFactory[ConnectionTypeConstants.Consumer];
            _domainEventFactory = domainEventFactory;
            _mediator = mediator;
            _consumer = Consumer;
        }
        public IModel Configure()
        {
            var channel = _consumerConnection.CreateModel();

            // configure this context as consumer
            _consumer.Configure(channel);

            var consumer = new EventingBasicConsumer(channel);

            // add my event handler to Received event in rmq
            consumer.Received += (model, ea) =>
            {
                // bytes to string 
                string body = Encoding.UTF8.GetString(ea.Body);

                // string to JObject
                JObject jBody = JObject.Parse(body);

                // get type of event
                DomainEventTypeConstants eventType = (DomainEventTypeConstants)(int)jBody["domainEventType"];

                // get event from domain event factory
                IDomainEvent targetDomainEvent = _domainEventFactory.Get(eventType, jBody["content"].ToObject<JObject>());

                // publish target event with mediator
                _mediator.Publish(targetDomainEvent);

                // acknowledge the delivery
                channel.BasicAck(ea.DeliveryTag, false);

            };

            // sign to this channel start service as consumers
            channel.BasicConsume(queue: _consumer.GetQueueName(), 
                                 autoAck: false,
                                 consumer: consumer);

            return channel;
        }

    }
}
