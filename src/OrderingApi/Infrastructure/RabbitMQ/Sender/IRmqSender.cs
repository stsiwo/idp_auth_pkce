using OrderingApi.Application.DomainEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Sender
{
    public interface IRmqSender
    {
        void Send<T>(T message) where T : IDomainEvent;
    }
}
