using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.Application.DomainEvent.Factory.Storage.CovarianceStorage
{
    public class KoreanMealFactory : IMealFactory<KoreanMeal>
    {
        public IMeal Cook()
        {
            return (IMeal)new KoreanMeal();
        }
    }
}
