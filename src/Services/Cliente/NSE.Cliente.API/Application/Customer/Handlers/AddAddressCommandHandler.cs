using Core.Messages;
using FluentValidation.Results;
using MediatR;
using NSE.Cliente.API.Application.Customer.Commands;
using NSE.Cliente.API.Domain.Entities;
using NSE.Cliente.API.Domain.Interfaces;
using NSE.WebApi.Core.User;

namespace NSE.Cliente.API.Application.Customer.Handlers
{
    public class AddAddressCommandHandler : CommandHandler, IRequestHandler<AddAddressCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAspNetUser _user;

        public AddAddressCommandHandler(ICustomerRepository customerRepository, IAspNetUser user)
        {
            _customerRepository = customerRepository;
            _user = user;
        }

        public async Task<ValidationResult> Handle(AddAddressCommand message, CancellationToken cancellationToken)
        {
            var validMessage = message.IsValid();

            if (!validMessage) return message.ValidationResult;

            var customerId = _user.GetUserId();

            var address = new Address(message.StreetAddress, message.BuildingNumber, message.SecondaryAddress, message.Neighborhood, message.ZipCode, message.City, message.State, customerId);

            _customerRepository.AddAddress(address);

            return await PersistData(_customerRepository.UnitOfWork);
        }
    }
}