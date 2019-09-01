using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.Application.DomainEvent.Factory.Storage.ContravarianceStorage
{
    class SaveCommandHandler : ITHandler<ICommand>
    {
        public string Handle(ICommand command)
        {
            return command.GetType().ToString();
        }
    }
}
