using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.Application.DomainEvent.Factory.Storage.CovarianceStorage
{
    public interface IMealFactory<out T> where T : IMeal
    {
        IMeal Cook();
    }
}
