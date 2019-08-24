using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingApiUnitTest.DI.TestEntity
{
    public class TargetEntity : ITargetEntity
    {
        public string TestMethod()
        {
            return "processing method";
        }
    }
}
