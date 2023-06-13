using NSE.Catalogo.API.Domain.Entities;

namespace NSE.Catalogo.API.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(Guid id);
        void Add(Product produto);
        void Update(Product produto);
        Task<IEnumerable<Product>> GetProducts(string ids);
    }
}