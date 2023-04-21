namespace Core.Specification.Validation
{
    public class Rule<T>
    {
        private readonly Specification<T> _specification;
        public string ErrorMessage { get; }

        public Rule(Specification<T> specification, string errorMessage)
        {
            _specification = specification;
            ErrorMessage = errorMessage;
        }

        public bool Validate(T obj)
        {
            return _specification.IsSatisfiedBy(obj);
        }
    }
}