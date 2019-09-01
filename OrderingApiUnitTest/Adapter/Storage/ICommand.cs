using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.Adapter.Storage
{
    public interface ICommand
    {
        string Handle();
    }
}
