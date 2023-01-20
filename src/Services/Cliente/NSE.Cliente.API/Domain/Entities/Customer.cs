using Core.DomainObjects;

namespace NSE.Cliente.API.Domain.Entities
{
    public class Customer : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public bool Excluded { get; private set; }
        public Address Address { get; private set; }
        protected Customer() { }
        public Customer(Guid id, string name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = new Email(email);
            Cpf = new Cpf(cpf);
            Excluded = false;
        }

        public void UpdateEmail(string email)
        {
            Email = new Email(email);
        }

        public void UpdateAddress(Address address)
        {
            Address = address;
        }
    }
}