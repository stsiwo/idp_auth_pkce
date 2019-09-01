using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.Adapter.Storage
{
    public class DeleteCommand : ICommand
    {
        public string Handle()
        {
            return "saved command";
        }
    }
}
