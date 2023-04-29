using Core.Messages;
using NSE.Cliente.API.Application.Customer.Validations;

namespace NSE.Cliente.API.Application.Customer.Commands
{
    public class AddAddressCommand : Command
    {
        public string Street { get; private set; }
        public string HouseNumber { get; private set; }
        public string Complement { get; private set; }
        public string Neighborhood { get; private set; }
        public string ZipCode { get; private set; }
        public string City { get; set; }
        public string State { get; set; }
        public Guid CustomerId { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new AddAddressValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}