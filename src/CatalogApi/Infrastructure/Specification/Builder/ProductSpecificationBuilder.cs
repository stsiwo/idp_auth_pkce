using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Core;
using CatalogApi.Infrastructure.Specification.Products;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Specification.Builder
{
    public class ProductSpecificationBuilder : SpecificationBuilder<DataEntity.Product>
    {
        public ProductSpecificationBuilder(ISpecification<DataEntity.Product> specification) : base(specification)
        {

        }
        public override Func<DataEntity.Product, bool> Build(IDictionary<string, string> qs)
        {
            // 1. map query string dictionary with specification
            foreach (KeyValuePair<string, string> query in qs)
            {
                if (query.Key.Equals(QueryConstants.KeyWord))
                {
                    this._BaseSpecification.And(new IncludeKeyWordSpecification(query.Value));
                }
                else if (query.Key.Equals(QueryConstants.MaxPrice))
                {
                    this._BaseSpecification.And(new PriceIsMoreThanOrEqualSpecification(Convert.ToDecimal(query.Value.ToString())));
                }
                else if (query.Key.Equals(QueryConstants.MinPrice))
                {
                    this._BaseSpecification.And(new PriceIsLessThanOrEqualSpecification(Convert.ToDecimal(query.Value.ToString())));
                }
                else if (query.Key.Equals(QueryConstants.ReviewScore))
                {
                    this._BaseSpecification.And(new IncludeReviewScoreSpecification((ScoreConstants)Enum.Parse(typeof(ScoreConstants), query.Value)));
                }
            }

            // 2. compile to Delegate
            return this._BaseSpecification.CompileToDelegate();

        }

    }
}
