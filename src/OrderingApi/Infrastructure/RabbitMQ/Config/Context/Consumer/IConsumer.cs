using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.Context.Consumer
{
    public interface IConsumer : IContext
    {
        string GetQueueName();
    }
}
