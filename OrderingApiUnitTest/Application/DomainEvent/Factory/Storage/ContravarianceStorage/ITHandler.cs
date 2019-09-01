using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.Application.DomainEvent.Factory.Storage.ContravarianceStorage
{
    public interface ITHandler<in T> where T : ICommand 
    {
        string Handle(T command);
    }
}
