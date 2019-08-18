using OrderingApi.Infrastructure.RabbitMQ.Config.Context.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderingApiUnitTest.DI.TestEntity
{
    class IPublisherTest
    {
        private IEnumerable<IRmqPublisher> _publishers;

        public IPublisherTest(IEnumerable<IRmqPublisher> rmqPublishers)
        {
            _publishers = rmqPublishers;
        }

        public int Count()
        {
            return _publishers.Count();
        }
    }
}
