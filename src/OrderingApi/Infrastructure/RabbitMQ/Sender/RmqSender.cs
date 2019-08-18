﻿using Autofac.Features.Indexed;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Infrastructure.RabbitMQ.Config;
using OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher;
using OrderingApi.Infrastructure.RabbitMQ.Message;
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

        private IPublisher _publisher; 

        public RmqSender(IIndex<ConnectionTypeConstants, IModel> channelFactory, IMapper mapper, IPublisher publisher)
        {
            _channel = channelFactory[ConnectionTypeConstants.Publisher];
            _mapper = mapper;
            _publisher = publisher;
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

            // map message to RmqMessage object
            RmqMessage rmqMessage = _mapper.Map<RmqMessage>(message);

            // RmqMessage => JObject (message)
            JObject rmqMessageJObject = JObject.FromObject(rmqMessage, camelCaseSerializer);

            // JObject (message) => json 
            string jsonMessage = JsonConvert.SerializeObject(rmqMessageJObject, Formatting.None, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            // json (message) => byte[]; 
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            // send message
            _channel.BasicPublish(exchange: _publisher.GetExchangeName(), 
                                 routingKey: routingKey, 
                                 basicProperties: properties,
                                 body: body);

        }
    }
}
