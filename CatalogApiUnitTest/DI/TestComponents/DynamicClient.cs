using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogApiUnitTest.DI.TestComponents
{
    class DynamicClient
    {
        public Func<string, DynamicComponent> _dComp;

        public DynamicClient(Func<string, DynamicComponent> dComp)
        {
            _dComp = dComp;
        }

        public string GetMessage()
        {
            var dc = _dComp("satoshi");
            return dc.GetMessage();
        }
    }
}
