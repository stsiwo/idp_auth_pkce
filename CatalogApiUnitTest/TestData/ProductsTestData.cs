using Bogus;
using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CatalogApiUnitTest.TestData
{
    public static class ProductsTestData
    {
        public static IList<Product> GetProducts()
        {
            var categoryFaker = new Faker<Category>()
                .RuleFor(c => c.Id, f => f.PickRandom<CategoryConstants>())
                .RuleFor(c => c.Title, (f, c) =>
                {
                    var _cons = (CategoryConstants)c.Id;
                    return _cons.ToString();
                })
                .RuleFor(c => c.Description, f => f.Random.Words(f.Random.Number(1,400)))
                .RuleFor(s => s.ImageURL, f => f.Image.PicsumUrl());

            var subCategoryFaker = new Faker<SubCategory>()
                .RuleFor(s => s.Id, f => f.PickRandom<SubCategoryConstants>())
                .RuleFor(s => s.Title, (f, c) =>
                {
                    var _cons = (SubCategoryConstants)c.Id;
                    return _cons.ToString();
                })
                .RuleFor(s => s.Description, f => f.Random.Words(f.Random.Number(1,400)))
                .RuleFor(s => s.ImageURL, f => f.Image.PicsumUrl())
                .RuleFor(s => s.Category, f => categoryFaker.Generate(1).First())
                .RuleFor(s => s.CategoryId, (f, s) => s.Category.Id);


            var subImageFaker = new Faker<SubImage>()
                .RuleFor(s => s.Id, f => Guid.NewGuid().ToString())
                .RuleFor(s => s.Url, f => f.Image.PicsumUrl());

            var reviewFaker = new Faker<Review>()
                .RuleFor(r => r.Id, f => Guid.NewGuid().ToString())
                .RuleFor(r => r.Author, f => f.Name.FindName())
                .RuleFor(r => r.Comment, f => f.Random.Words(f.Random.Number(1, 500)))
                .RuleFor(r => r.Score, f => f.PickRandom<ScoreConstants>());
                //.RuleFor(r => r.ProductId, f => f.Product.Id); // you can't access to Product at this moment!! use "FinishWith" instead to assign product id to this
                

            var productFaker = new Faker<Product>()
                .RuleFor(p => p.Id, f => Guid.NewGuid().ToString())
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Random.Words(f.Random.Number(1, 400)))
                .RuleFor(p => p.MainImageURL, f => f.Image.PicsumUrl())
                .RuleFor(p => p.SubImages, f => subImageFaker.Generate(4).ToList())
                .RuleFor(p => p.Price, f => Decimal.Parse(f.Commerce.Price(100m, 100000m), NumberStyles.Currency))
                .RuleFor(p => p.SubCategory, f => subCategoryFaker.Generate(1).First())
                .RuleFor(p => p.SubCategoryId, (f, p) => p.SubCategory.Id) 
                .RuleFor(p => p.Reviews, (f, p) => reviewFaker.Generate(f.Random.Number(0, 10)))
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


            Product SeededProduct(int seed)
            {
                return productFaker.UseSeed(seed).Generate();
            }

            var products = Enumerable.Range(1,50)
                .Select(SeededProduct)
                .ToList();

            return products;  
        }
    }
}
