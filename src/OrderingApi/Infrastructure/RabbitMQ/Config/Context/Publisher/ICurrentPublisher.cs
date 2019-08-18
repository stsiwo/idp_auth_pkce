using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher
{
    // current (this project publisher): need to distinguish from the non-current (the other project publisher)
    public interface ICurrentPublisher : IPublisher
    {
        // marker
    }
}
