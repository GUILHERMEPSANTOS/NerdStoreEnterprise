using NSE.Catalogo.API.Domain.Enitites;
using NSE.Catalogo.API.Domain.Entities;

namespace NSE.Catalogo.API.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<PagedResult<Product>> GetPagedProducts(int pagedSize, int pagedIndex, string query);
        Task<Product> GetById(Guid id);
        void Add(Product produto);
        void Update(Product produto);
        Task<IEnumerable<Product>> GetProducts(string ids);
    }
}