
using FluentValidation.Results;

namespace Core.Specification.Validation
{
    public abstract class SpecValidator<T>
    {
        private readonly IDictionary<string, Rule<T>> _validations = new Dictionary<string, Rule<T>>();
        public ValidationResult ValidationResult = new ValidationResult();

        public ValidationResult Validate(T obj)
        {
            foreach (var rule in _validations.Keys)
            {
                var validation = _validations[rule];
                var isValid = validation.Validate(obj);

                if (!isValid)
                {
                    ValidationResult.Errors.Add(new ValidationFailure(obj?.GetType().Name ?? string.Empty, validation.ErrorMessage));
                }
            }

            return ValidationResult;
        }

        protected void Add(string ruleName, Rule<T> rule)
        {
            _validations.Add(ruleName, rule);
        }

        protected void Remove(string ruleName)
        {
            _validations.Remove(ruleName);
        }

        protected Rule<T> GetRule(string roleName)
        {
            _validations.TryGetValue(roleName, out var role);

            return role;
        }
    }
}