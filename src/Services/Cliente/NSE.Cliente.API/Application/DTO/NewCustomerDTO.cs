namespace NSE.Cliente.API.Application.DTO
{
    public class NewCustomerDTO
    {
        public Guid Id { get;  set; }
        public string Name { get;  set; }
        public string Email { get;  set; }
        public string Cpf { get;  set; }
    }
}