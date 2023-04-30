using Core.Messages;
using NSE.Cliente.API.Application.Customer.Validations;

namespace NSE.Cliente.API.Application.Customer.Commands
{
    public class AddAddressCommand : Command
    {
        public string StreetAddress { get; private set; }
        public string BuildingNumber { get; private set; }
        public string SecondaryAddress { get; private set; }
        public string Neighborhood { get; private set; }
        public string ZipCode { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new AddAddressValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}