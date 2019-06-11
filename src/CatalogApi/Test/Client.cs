using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Test
{
    public class Client
    {
        private readonly IDep _dep;

        public Client(IDep dep)
        {
            _dep = dep;
        }
    }
}
