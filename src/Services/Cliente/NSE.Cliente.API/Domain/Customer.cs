using Core.DomainObjects;

namespace NSE.Cliente.API.Domain
{
    public class Customer : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public bool Excluded { get; private set; }
        public Address Address { get; private set; }
        protected Customer() { }
        public Customer(string name, string email, string cpf, bool excluded)
        {
            Name = name;
            Email = email;
            Cpf = cpf;
            Excluded = false;
        }
    }
}