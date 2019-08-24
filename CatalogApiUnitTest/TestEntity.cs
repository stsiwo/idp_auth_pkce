using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogApiUnitTest
{
    class TestEntity : IDataEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
