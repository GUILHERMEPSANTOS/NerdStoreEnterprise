using FluentValidation.Results;
using NSE.Cliente.API.Application.DTO;

namespace NSE.Cliente.API.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<ValidationResult> Add(NewCustomerDTO newCustomerDTO);
    }
}