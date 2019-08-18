using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Application.Repository
{
    public interface IRepository<T>
        where T : IAggregate
    {
        T Create(T e);

        T Find(Guid id);

        Task<T> FindAsync(Guid id);
    }
}
