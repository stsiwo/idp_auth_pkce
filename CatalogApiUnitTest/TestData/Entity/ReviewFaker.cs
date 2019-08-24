using Bogus;
using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApiUnitTest.TestData.Entity
{
    public class ReviewFaker
    {
        public static Faker<Review> GetReviewFaker()
        {

            var reviewFaker = new Faker<Review>()
                .RuleFor(r => r.Id, f => Guid.NewGuid().ToString())
                .RuleFor(r => r.Author, f => f.Name.FindName())
                .RuleFor(r => r.Comment, f => f.Random.Words(f.Random.Number(1, 500)))
                .RuleFor(r => r.Score, f => f.PickRandom<ReviewScoreConstants>())
                .RuleFor(r => r.CreationDate, f => f.Date.Past());

            return reviewFaker;
        }

        public static IList<Review> GetRandomReviewList(int max)
        {
            var faker = GetReviewFaker();

            Review SeededReview(int seed)
            {
                return faker.UseSeed(seed).Generate();
            }

            Random rnd = new Random();
            int maxNumberOfInstances = rnd.Next(0, max);

            var reviews = Enumerable.Range(1, maxNumberOfInstances)
                .Select(SeededReview)
                .ToList();

            return reviews;  
        }

        public static IList<Review> GetReviewList(int amount)
        {
            var faker = GetReviewFaker();

            Review SeededReview(int seed)
            {
                return faker.UseSeed(seed).Generate();
            }

            var reviews = Enumerable.Range(1, amount)
                .Select(SeededReview)
                .ToList();

            return reviews;  
        }
        
    }
}
