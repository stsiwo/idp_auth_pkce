using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.Application.DomainEvent.Factory.Storage.CovarianceStorage
{
    public class JapaneseMealFactory<T> : IMealFactory<T> where T : JapaneseMeal
    {
        public IMeal Cook()
        {
            return (IMeal)new JapaneseMeal();
        }
    }
}
