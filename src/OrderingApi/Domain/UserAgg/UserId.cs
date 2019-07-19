using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.UserAgg
{
    public class UserId : IValueObject
    {
        public readonly string Id;

        public UserId()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
