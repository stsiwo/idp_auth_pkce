using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Repository
{
    public interface IRepository<T, I> 
        where T : IAggregate
        where I : IValueObject
    {
        // return new Guid Id for this Aggregate
        I NextIdentity();

        T Add(T aggregate);

        Task<T> FindAsync();


    }
}
