using CatalogApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.Specification.Core
{
    public class AndSpecification<T> : CompositeSpecification<T> where T : IDataEntity
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this._left = left;
            this._right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();

            /**
             * need to use Expression.Invoke method rather than rightExpression.Body
             * when you need to compile combined Expression into Delete:
             * need to review: https://stackoverflow.com/questions/15589239/linq-expressions-variable-p-of-type-referenced-from-scope-but-it-is-not-defi/15592610
             **/
             // Invoke method is just invoking the expression with specified argument (in this case, use left specficiation's 'p' argument)
            BinaryExpression andExpression = Expression.AndAlso(leftExpression.Body, Expression.Invoke(rightExpression, leftExpression.Parameters.Single()));

            // create expression tree and return it
            return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters.Single());
        }

    }
}
