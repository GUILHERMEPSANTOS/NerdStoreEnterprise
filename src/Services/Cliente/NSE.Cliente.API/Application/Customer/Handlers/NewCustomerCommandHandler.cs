using MediatR;
using FluentValidation.Results;
using NSE.Cliente.API.Application.Customer.Commands;


namespace NSE.Cliente.API.Application.Customer.Handlers
{
    public class NewCustomerCommandHandler : IRequestHandler<NewCustomerCommand, ValidationResult>
    {
        public async Task<ValidationResult> Handle(NewCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;


            return message.ValidationResult;
        }
    }
}