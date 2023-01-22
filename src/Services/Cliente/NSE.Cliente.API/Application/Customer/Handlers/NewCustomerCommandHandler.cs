using FluentValidation.Results;
using MediatR;
using Core.Messages;
using NSE.Cliente.API.Application.Customer.Commands;
using NSE.Cliente.API.Domain.Interfaces;
using NSE.Cliente.API.Application.Customer.Events;

namespace NSE.Cliente.API.Application.Customer.Handlers
{
    public class NewCustomerCommandHandler : CommandHandler, IRequestHandler<NewCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public NewCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ValidationResult> Handle(NewCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var customer = new NSE.Cliente.API.Domain.Entities.Customer(message.Id, message.Name, message.Email, message.Cpf);

            var customerExist = await _customerRepository.GetByCpf(customer.Cpf.Number);

            if (customerExist is not null)
            {
                AddError("Este CPF já está em uso");
                return ValidationResult;
            }

            _customerRepository.Add(customer);

            customer.AddEvent(new NewCustomerAddedEvent(message.Id, message.Name, message.Email, message.Cpf));

            return await PersistData(_customerRepository.UnitOfWork);
        }
    }
}