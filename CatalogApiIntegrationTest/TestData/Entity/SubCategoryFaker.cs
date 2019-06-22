using Bogus;
using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApiIntegrationTest.TestData.Entity
{
    public class SubCategoryFaker
    {
        public static Faker<SubCategory> GetSubCategoryFaker()
        {
            int totalCategories = Enum.GetNames(typeof(CategoryConstants)).Length;
            var categories = CategoryFaker.GetCategoryList(totalCategories);
            int categoryId = 0;
            int subCategoryId = 0;


            var subCategoryFaker = new Faker<SubCategory>()
                .RuleFor(s => s.Id, f => (SubCategoryConstants)subCategoryId++)
                .RuleFor(s => s.Title, (f, c) =>
                {
                    var _cons = (SubCategoryConstants)c.Id;
                    return _cons.ToString();
                })
                .RuleFor(s => s.Description, f => f.Random.Words(f.Random.Number(1,400)))
                .RuleFor(s => s.ImageURL, f => f.Image.PicsumUrl())
                .RuleFor(s => s.Category, f => 
                {
                    var category = categories.Where(c => (int)c.Id == categoryId).FirstOrDefault();
                    categoryId = (categoryId >= totalCategories - 1) ? 0 : categoryId + 1;
                    return category;
                })
                .RuleFor(s => s.CategoryId, (f, s) => s.Category.Id);

            return subCategoryFaker;
        }

        public static IList<SubCategory> GetSubCategoryList(int amount)
        {
            if (Enum.GetNames(typeof(SubCategoryConstants)).Length < amount)
                amount = Enum.GetNames(typeof(SubCategoryConstants)).Length;

            var faker = GetSubCategoryFaker();

            SubCategory SeededSubCategory(int seed)
            {
                return faker.UseSeed(seed).Generate();
            }

            var subcategories = Enumerable.Range(1,amount)
                .Select(SeededSubCategory)
                .ToList();

            return subcategories;  
        }
    }
}
