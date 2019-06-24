using Bogus;
using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApiUnitTest.TestData.Entity
{
    public static class CategoryFaker
    {
        public static Faker<Category> GetCategoryFaker()
        {
            int categoryId = 0;
            var categoryFaker = new Faker<Category>()
                .RuleFor(c => c.Id, f => (CategoryConstants)categoryId++)
                .RuleFor(c => c.Title, (f, c) =>
                {
                    var _cons = (CategoryConstants)c.Id;
                    return _cons.ToString();
                })
                .RuleFor(c => c.Description, f => f.Random.Words(f.Random.Number(1,400)))
                .RuleFor(s => s.ImageURL, f => f.Image.PicsumUrl());

            return categoryFaker;
        }

        public static IList<Category> GetCategoryList(int amount)
        {
            if (Enum.GetNames(typeof(CategoryConstants)).Length < amount)
                amount = Enum.GetNames(typeof(CategoryConstants)).Length;

            var faker = GetCategoryFaker();

            Category SeededCategory(int seed)
            {
                return faker.UseSeed(seed).Generate();
            }

            var categories = Enumerable.Range(1,amount)
                .Select(SeededCategory)
                .ToList();

            return categories;  
        }
    }
}
