using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Specification.Product
{
    public class IncludeReviewScoreSpecification : CompositeSpecification<DataEntity.Product>
    {
        private readonly ScoreConstants _Score;

        public IncludeReviewScoreSpecification(ScoreConstants score)
        {
            this._Score = score;
        }
        public override Expression<Func<DataEntity.Product, bool>> ToExpression()
        {
            return product => Convert.ToInt16(product.Reviews.Average(r => r.Score)) == (int)this._Score;
        }
    }
}
