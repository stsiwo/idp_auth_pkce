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

        private readonly IMapper _mapper;  
        public CartQuery(ISession session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        public async Task<IList<CartModel>> GetCartsByIds(IList<string> ids)
        {
            var results = await _session
                        .Query<Cart>()
                        .Where(c => ids.Contains(c.Id.ToString()))
                        .Select(c => c)
                        .ToListAsync();

            return _mapper.Map<IList<CartModel>>(results);
        }
    }
}
