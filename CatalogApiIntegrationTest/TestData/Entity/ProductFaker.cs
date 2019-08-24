using Bogus;
using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApiIntegrationTest.TestData.Entity
{
    public class ProductFaker
    {
        public static Faker<Product> GetProductFaker()
        {
            int totalSubCategories = Enum.GetNames(typeof(SubCategoryConstants)).Length;
            var subCategories = SubCategoryFaker.GetSubCategoryList(totalSubCategories);
            var subImageFaker = SubImageFaker.GetSubImageFaker();
            var subCategoryId = 0;

            var productFaker = new Faker<Product>()
                .RuleFor(p => p.Id, f => Guid.NewGuid().ToString())
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Random.Words(f.Random.Number(1, 400)))
                .RuleFor(p => p.MainImageURL, f => f.Image.PicsumUrl())
                .RuleFor(p => p.SubImages, f => subImageFaker.Generate(f.Random.Number(0, 4)))
                .RuleFor(p => p.Price, f => Decimal.Parse(f.Commerce.Price(100m, 100000m), NumberStyles.Currency))
                .RuleFor(p => p.SubCategory, f => 
                {
                    var subCategory =  subCategories.Where(c => (int)c.Id == subCategoryId).FirstOrDefault();
                    subCategoryId = (subCategoryId >= totalSubCategories - 1) ? 0 : subCategoryId + 1;
                    return subCategory;

                })
                .RuleFor(p => p.SubCategoryId, (f, p) => p.SubCategory.Id) 
                .RuleFor(p => p.Reviews, (f, p) => ReviewFaker.GetRandomReviewList(f.Random.Number(0, 10)))
                .RuleFor(p => p.CreationDate, f => f.Date.Past())
                .FinishWith((f, p) => 
                {
                   // assign product id to each review and subImage entities 
                   foreach (var review in p.Reviews)
                    {
                        review.ProductId = p.Id;
                    } 

                   foreach (var subImage in p.SubImages)
                    {
                        subImage.ProductId = p.Id;
                    } 
                }); 

            return productFaker;
        }

        public static IList<Product> GetProductList(int amount)
        {

            var faker = GetProductFaker();

            Product SeededProduct(int seed)
            {
                return faker.UseSeed(seed).Generate();
            }

            var products = Enumerable.Range(1,amount)
                .Select(SeededProduct)
                .ToList();

            return products;  
        }
    }
}
