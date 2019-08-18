using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher
{
    public interface IPublisher : IContext
    {
        string ExchangeName { get; }

        string RoutingKey { get; }

        void DeclareExchangeIn(IModel channel);

    }
}
