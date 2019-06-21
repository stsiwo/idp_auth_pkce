using Bogus;
using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApiIntegrationTest.TestData.Entity
{
    public class SubImageFaker
    {
        public static Faker<SubImage> GetSubImageFaker()
        {
            var subImageFaker = new Faker<SubImage>()
                .RuleFor(s => s.Id, f => Guid.NewGuid().ToString())
                .RuleFor(s => s.Url, f => f.Image.PicsumUrl());

            return subImageFaker;
        }

        public static IList<SubImage> GetSubImageList(int amount)
        {
            var faker = GetSubImageFaker();

            SubImage SeededSubImage(int seed)
            {
                return faker.UseSeed(seed).Generate();
            }

            var subImages = Enumerable.Range(1,amount)
                .Select(SeededSubImage)
                .ToList();

            return subImages;  
        }
    }
}
