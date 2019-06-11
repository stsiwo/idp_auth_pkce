using CatalogApi.Infrastructure;
using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApiIntegrationTest.Helpers
{
    public static class Utilities
    {
        public static void InitializeDbForTests(CatalogApiDbContext db)
        {
            db.Products.AddRange(GetSeedingProducts());
            db.SaveChanges();
        }

        public static List<Product> GetSeedingProducts()
        {
            return new List<Product>()
            {
                new Product(){ Name = "product1_name", Description = "product1_description"},
            };
        }
    }
}
