using FluentValidation.Results;

namespace Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult { get; set; }

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }
    }
}