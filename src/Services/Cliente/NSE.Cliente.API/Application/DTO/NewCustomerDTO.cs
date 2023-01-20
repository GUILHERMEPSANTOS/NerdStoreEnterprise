namespace NSE.Cliente.API.Application.DTO
{
    public class NewCustomerDTO
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
    }
}