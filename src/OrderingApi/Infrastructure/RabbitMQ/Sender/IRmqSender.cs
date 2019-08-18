using OrderingApi.Application.DomainEvent;
using OrderingApi.Infrastructure.RabbitMQ.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Sender
{
    public interface IRmqSender
    {
        void Send<T>(T message, string routingKey) where T : IDomainEvent;
    }
}
