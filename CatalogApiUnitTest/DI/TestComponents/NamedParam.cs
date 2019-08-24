using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogApiUnitTest.DI.TestComponents
{
    class NamedParam : INamedParam
    {
        public string Name;

        public NamedParam(string name)
        {
            Name = name;
        }

        public string GetName()
        {
            return Name;
        }
    }
}
