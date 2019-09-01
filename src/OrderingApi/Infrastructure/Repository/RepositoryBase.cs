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
        public virtual Guid Create(T e)
        {
            Guid id;
            using(var tx = _session.BeginTransaction())
            {
                id = (Guid)_session.Save(e);
            }
            return id;
        }

        public T Find(Guid id)
        {
            T target; 
            using(var tx = _session.BeginTransaction())
            {
                target = _session.Get<T>(id);
                // #DOUBT
                //tx.Commit();
            }
            return target;
        }

        public async Task<T> FindAsync(Guid id)
        {
            T target;
            using(var tx = _session.BeginTransaction())
            {
                target = await _session.GetAsync<T>(id);
                //tx.Commit();
            }
            return target;
        }
    }
}
