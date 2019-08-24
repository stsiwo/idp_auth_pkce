using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace OrderingApiUnitTest.MediatR.TestEntity
{
    public class DEHandler : INotificationHandler<IDEvent>
    {
        private readonly ITestOutputHelper _output;

        public DEHandler(ITestOutputHelper output)
        {
            _output = output;
        }

        public Task Handle(IDEvent notification, CancellationToken cancellationToken)
        {
            _output.WriteLine("all event handler is called");
            return Task.CompletedTask;
        }
    }
}
