namespace NSE.Cliente.API.Application.DTO
{
    public class AddressDTO
    {
        public string Street { get; private set; }
        public string HouseNumber { get; private set; }
        public string Complement { get; private set; }
        public string Neighborhood { get; private set; }
        public string ZipCode { get; private set; }
        public string City { get; set; }
        public string State { get; set; }
        public Guid CustomerId { get; private set; }
    }
}