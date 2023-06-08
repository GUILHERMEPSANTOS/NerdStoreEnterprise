using FluentValidation.Results;

namespace NSE.Pagamento.API.Services;
public abstract class ServiceBase
{
    protected ValidationResult validationResult { get; set; }

    protected ServiceBase()
    {
        validationResult = new ValidationResult();
    }

    protected void AddError(string errorMessage, string propertyName = "")
    {
        validationResult.Errors.Add(
            new ValidationFailure(
                    propertyName: propertyName,
                    errorMessage: errorMessage
            )
        );
    }

}
