using Core.Data;
using Microsoft.EntityFrameworkCore;
using NSE.Cliente.API.Domain.Entities;
using NSE.Cliente.API.Domain.Interfaces;

namespace NSE.Cliente.API.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _customerContext;
        public IUnitOfWork UnitOfWork => _customerContext;

        public CustomerRepository(CustomerContext customerContext)
        {
            _customerContext = customerContext;
        }

        public void Add(Customer customer)
        {
            _customerContext.Customers.Add(customer);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _customerContext.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<Customer> GetByCpf(string cpf)
        {
            return await _customerContext.Customers.FirstOrDefaultAsync(customer => customer.Cpf.Number == cpf);
        }

        public void Dispose()
        {
            _customerContext?.Dispose();
        }
    }
}