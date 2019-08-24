using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Specification.Products
{
    public class IncludeReviewScoreSpecification : CompositeSpecification<Product>
    {
        private readonly ReviewScoreConstants _Score;

        public IncludeReviewScoreSpecification(string score)
        {
            this._Score = (ReviewScoreConstants)Convert.ToInt32(score);
        }
        public override Expression<Func<Product, bool>> ToExpression()
        {
            return product => Convert.ToInt16(product.Reviews.DefaultIfEmpty().Average(r => (int) r.Score)) == (int)this._Score;
        }
    }
}
