using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Test
{
    public class Dep : IDep
    {
        public void DepMethod()
        {
            Console.WriteLine("hey");
        }
    }
}
