using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.TestAgg
{
    public class Test : IEntity
    {
        public string Password { get; set; }
        public decimal Price { get; set; }
    }
}
