using NHibernate;
using OrderingApi.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.Repository
{
    public class OrderRepository : RepositoryBase<Order>
    {
        public OrderRepository(ISession session) : base(session)
        {
        }
    }
}
