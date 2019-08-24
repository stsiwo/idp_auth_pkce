using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogApiUnitTest.DI.TestComponents
{
    class DynamicComponent
    {
        public string Name { get; set; }

        public DynamicComponent(string name)
        {
            Name = name;
        }
        public string GetMessage()
        {
            return Name;
        }
    }
}
