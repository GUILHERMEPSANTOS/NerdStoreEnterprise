using Core.DomainObjects.Data;
using NSE.Cliente.API.Domain.Entities;

namespace NSE.Cliente.API.Domain.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        void Add(Customer customer);
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetByCpf(string cpf);
        Task<Address> GetAddressBy(Guid customerId);
        void AddAddress(Address address);
    }
}