using Autofac.Features.Indexed;
using MediatR;
using Newtonsoft.Json.Linq;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Infrastructure.RabbitMQ.Config.AntiCorruption;
using OrderingApi.Infrastructure.RabbitMQ.Config.Context.Consumer;
using OrderingApi.Infrastructure.RabbitMQ.Message;
using OrderingApi.Infrastructure.Repository.MessageStorage.Consuming;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OrderingApi.Infrastructure.RabbitMQ.Config
{
    public class ConsumerChannelConfig
    {
        private IConnection _consumerConnection;

        private IDomainEventAdapter _domainEventFactory;

        private IMediator _mediator;

        private IConsumer _consumer;

        private IConsumedMessageStore _consumedMessageStore;

        public ConsumerChannelConfig(IIndex<ConnectionTypeConstants, IConnection> connectionFactory, IDomainEventAdapter domainEventFactory, IMediator mediator, IConsumer Consumer, IConsumedMessageStore consumedMessageStore)
        {
            _consumerConnection = connectionFactory[ConnectionTypeConstants.Consumer];
            _domainEventFactory = domainEventFactory;
            _mediator = mediator;
            _consumer = Consumer;
            _consumedMessageStore = consumedMessageStore;
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

                bool isProcessed = false;

                // txs must be share with domain event handler dispatched following 
                using(var scope = new TransactionScope())
                {
                    RmqConsumeMessage processedMessage = _consumedMessageStore.GetByMessageId(Guid.Parse((string)jBody["messageId"]));

                    // if message already exists, this means the message processed before
                    if (processedMessage != null) isProcessed = true;
                }

                if (isProcessed)
                {
                    // return ack to delete redelivered message in queue
                    channel.BasicAck(ea.DeliveryTag, false);
                }
                else
                { 
                    // get type of event
                    DomainEventTypeConstants eventType = (DomainEventTypeConstants)(int)jBody["domainEventType"];

                    // get event from domain event factory
                    IDomainEvent targetDomainEvent = _domainEventFactory.Get(eventType, jBody["content"].ToObject<JObject>());

                    // publish target event with mediator
                    _mediator.Publish(targetDomainEvent);

                    // acknowledge the delivery
                    channel.BasicAck(ea.DeliveryTag, false);
                }
            };

            // sign to this channel start service as consumers
            channel.BasicConsume(queue: _consumer.GetQueueName(), 
                                 autoAck: false,
                                 consumer: consumer);

            return channel;
        }

    }
}
