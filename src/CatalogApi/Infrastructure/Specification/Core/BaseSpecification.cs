using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Specification.Core
{
    /**
     * this is for surrogate specification to setup initial specification 
     * therefore, alway return true;
     **/
    public class BaseSpecification<T> : CompositeSpecification<T> where T : IDataEntity
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return entity => true;
        }
    }
}
