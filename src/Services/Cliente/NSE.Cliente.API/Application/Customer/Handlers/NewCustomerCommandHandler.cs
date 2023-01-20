using Core.Messages;
using FluentValidation.Results;
using MediatR;
using NSE.Cliente.API.Application.Customer.Commands;

namespace NSE.Cliente.API.Application.Customer.Handlers
{
    public class NewCustomerCommandHandler : CommandHandler, IRequestHandler<NewCustomerCommand, ValidationResult>
    {
        public async Task<ValidationResult> Handle(NewCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var costomer = new NSE.Cliente.API.Domain.Entities.Customer(message.Id, message.Name, message.Email, message.Cpf);

            //Validações de negocio


            //Persistir no banco

            if(true)
            {
                AddError("Este CPF já está em uso");
                return ValidationResult;
            }

            return message.ValidationResult;
        }
    }
}