using Core.Data;
using FluentValidation.Results;

namespace NSE.Carrinho.Api.Application.Services
{
    public abstract class ServiceBase
    {
        protected ValidationResult ValidationResult { get; set; }

        protected ServiceBase()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected async Task<ValidationResult> PersistData(IUnitOfWork uow)
        {
            if (!await uow.Commit()) AddError("Houve um erro ao persistir os dados");

            return ValidationResult;
        }

    }
}