using NHibernate;
using OrderingApi.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.Repository
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(ISession session) : base(session)
        {
        }
    }
}
