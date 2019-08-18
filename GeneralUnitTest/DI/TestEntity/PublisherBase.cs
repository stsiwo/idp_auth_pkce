using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralUnitTest.DI.TestEntity
{
    class PublisherBase : IPublisher
    {
        public string Name { get; }
        public string Price { get; }

        public PublisherBase(string name, string price)
        {
            Name = name;
            Price = price;
        }
    }
}
