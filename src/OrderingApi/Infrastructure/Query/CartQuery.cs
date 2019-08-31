using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NHibernate;
using NHibernate.Linq;
using OrderingApi.Application.Query;
using OrderingApi.Domain.CartAgg;
using OrderingApi.UI.Model;

namespace OrderingApi.Infrastructure.Query
{
    public class CartQuery : ICartQuery
    {
        private readonly ISession _session;

        public CartQuery(ISession session)
        {
            _session = session;
        }

        public async Task<IList<Cart>> GetCartsByIds(IList<Guid> ids)
        {
            IList<Cart> result = null;

            using(var tx = _session.BeginTransaction())
            {
                result = await _session
                        .Query<Cart>()
                        .Where(c => ids.Contains(c.Id))
                        .Select(c => c)
                        .ToListAsync();

                tx.Commit();
            }

            return result; 
        }
    }
}
