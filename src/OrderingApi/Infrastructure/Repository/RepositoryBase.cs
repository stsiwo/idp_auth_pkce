using NHibernate;
using OrderingApi.Application.DomainEvent;
using OrderingApi.Application.Repository;
using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T>
        where T : IAggregate
    {
        protected ISession _session;

        public RepositoryBase(ISession session)
        {
            _session = session;
        }
        public virtual T Create(T e)
        {
            return (T)_session.Save(e);
        }

        public T Find(Guid id)
        {
            return _session.Get<T>(id);
        }

        public async Task<T> FindAsync(Guid id)
        {
            return await _session.GetAsync<T>(id);
        }
    }
}
