using Core.DomainObjects;

namespace NSE.Cliente.API.Domain.Entities
{
    public class Address : Entity
    {
        public string Street { get; private set; }
        public string HouseNumber { get; private set; }
        public string Complement { get; private set; }
        public string Neighborhood { get; private set; }
        public string ZipCode { get; private set; }
        public string City { get; set; }
        public string State { get; set; }
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; }

        public Address(string street, string houseNumber, string complement, string neighborhood, string zipCode, string city, string state)
        {
            Street = street;
            HouseNumber = houseNumber;
            Complement = complement;
            Neighborhood = neighborhood;
            ZipCode = zipCode;
            City = city;
            State = state;
        }
    }
}