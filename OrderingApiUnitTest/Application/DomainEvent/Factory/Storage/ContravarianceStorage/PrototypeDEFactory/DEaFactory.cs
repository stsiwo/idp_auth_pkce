using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.Application.DomainEvent.Factory.Storage.ContravarianceStorage.PrototypeDEFactory
{
    public class DEaFactory : IDEFac<ICommandT, IModelT>
    {
        public IDOT Generate(ICommandT command, IModelT model)
        {
            return new DEa();
        }
    }
}
