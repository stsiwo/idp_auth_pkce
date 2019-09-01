using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.Application.DomainEvent.Factory.Storage.ContravarianceStorage.PrototypeDEFactory
{
    public interface IDEFac<in C, in M>
        where C : ICommandT
        where M : IModelT
    {
        IDOT Generate(C command, M model);
    }
}
