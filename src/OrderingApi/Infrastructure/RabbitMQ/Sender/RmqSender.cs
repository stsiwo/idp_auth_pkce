﻿using Autofac.Features.Indexed;
using AutoMapper;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Infrastructure.RabbitMQ.Config;
using OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher;
using OrderingApi.Infrastructure.RabbitMQ.Message;
using OrderingApi.Infrastructure.Repository;
using OrderingApi.Infrastructure.Repository.MessageStorage.Publishing;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Sender
{
    public class RmqSender : IRmqSender
    {
        private IModel _channel;

        private IMapper _mapper;

        private ICurrentPublisher _publisher;

        private IPublishedMessageStore _publishedMessageStore;

        private static readonly ILog log = LogManager.GetLogger(typeof(RmqSender));

        public RmqSender(IIndex<ConnectionTypeConstants, IModel> channelFactory, IMapper mapper, ICurrentPublisher publisher, IPublishedMessageStore publishedMessageStore)
        {
            _channel = channelFactory[ConnectionTypeConstants.Publisher];
            _mapper = mapper;
            _publisher = publisher;
            _publishedMessageStore = publishedMessageStore;
        }
        public void Send<D>(D message, string routingKey) where D : IDomainEvent
        {
            var camelCaseSerializer = new JsonSerializer()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            // set property of message
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            // map message to RmqPublishMessage object
            RmqPublishMessage rmqMessage = _mapper.Map<RmqPublishMessage>(message);

            // set delivery tag 
            rmqMessage.DeliveryTag = _channel.NextPublishSeqNo;

            using(var tx = _publishedMessageStore.BeginTransaction())
            {
                _publishedMessageStore.Create(rmqMessage);

                _publishedMessageStore.Commit(tx);
            }

            // RmqPublishMessage => JObject (message)
            JObject rmqMessageJObject = JObject.FromObject(rmqMessage, camelCaseSerializer);

            // JObject (message) => json 
            string jsonMessage = JsonConvert.SerializeObject(rmqMessageJObject, Formatting.None, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            log.Debug("the message is about sent: ");
            log.Debug(jsonMessage);

            // json (message) => byte[]; 
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            // send message
            _channel.BasicPublish(exchange: _publisher.ExchangeName, 
                                 routingKey: routingKey, 
                                 mandatory: true,
                                 basicProperties: properties,
                                 body: body);


        }
    }
}
