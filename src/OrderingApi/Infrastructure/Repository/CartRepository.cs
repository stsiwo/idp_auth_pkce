using NHibernate;
using OrderingApi.Domain.CartAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.Repository
{
    public class CartRepository : RepositoryBase<Cart>
    {
        public CartRepository(ISession session) : base(session)
        {
        }
    }
}
