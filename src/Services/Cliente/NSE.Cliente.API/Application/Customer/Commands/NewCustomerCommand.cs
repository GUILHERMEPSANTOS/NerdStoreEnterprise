using Core.Messages;
using NSE.Cliente.API.Application.Customer.Validations;

namespace NSE.Cliente.API.Application.Customer.Commands
{
    public class NewCustomerCommand : Command
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public NewCustomerCommand(Guid id, string name, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public override bool IsValid()
        {
            ValidationResult = new NewCustomerValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}