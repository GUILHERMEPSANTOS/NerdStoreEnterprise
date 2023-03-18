using System.Linq.Expressions;

namespace Core.Specification
{
    public class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _specification;

        public NotSpecification(Specification<T> specification)
        {
            _specification = specification;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expressions = _specification.ToExpression();
            var notExpression = Expression.Not(expressions.Body);

            return (Expression<Func<T, bool>>)Expression.Lambda(notExpression, expressions.Parameters.Single());
        }
    }
}
