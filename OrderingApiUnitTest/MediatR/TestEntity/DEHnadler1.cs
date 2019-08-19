using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace OrderingApiUnitTest.MediatR.TestEntity
{
    public class DEHandler1 : INotificationHandler<DEvent1>
    {
        private readonly ITestOutputHelper _output;

        public DEHandler1(ITestOutputHelper output)
        {
            _output = output;
        }

        public Task Handle(DEvent1 notification, CancellationToken cancellationToken)
        {
            _output.WriteLine("event 1 handler is called");
            return Task.CompletedTask;
        }
    }
}
