using OrderingApi.Domain.UserAgg;
using OrderingApi.Domain.Validator.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.Validator.Specification.UserAgg.Password
{
    public interface IUserPasswordSpecification : ISpecification<User>
    {
        // marker interface
    }
}
