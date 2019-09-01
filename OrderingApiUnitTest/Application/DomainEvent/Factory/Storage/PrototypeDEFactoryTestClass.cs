using Autofac.Features.Indexed;
using OrderingApiUnitTest.Application.DomainEvent.Factory.Storage.ContravarianceStorage.PrototypeDEFactory;
using System;

namespace OrderingApiUnitPrototypeDEFactoryTest.Application.DomainEvent.Factory.Storage
{
    class PrototypeDEFactoryTestClass
    {
        private readonly IIndex<int, IDEFac<ICommandT, IModelT>> _dEFac; 

        public PrototypeDEFactoryTestClass(IIndex<int, IDEFac<ICommandT, IModelT>> dEFac)
        {
            _dEFac = dEFac;
        }

        public Type Test(ICommandT command, IModelT model, int myint)
        {
            return _dEFac[myint].Generate(command, model).GetType();
        }
    }
}
