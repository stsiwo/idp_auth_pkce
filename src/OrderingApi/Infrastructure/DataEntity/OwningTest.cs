using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.DataEntity
{
    public class OwningTest
    {
        public int Id { get; set; }
        public OwnedTest Name { get; set; }
    }
}
