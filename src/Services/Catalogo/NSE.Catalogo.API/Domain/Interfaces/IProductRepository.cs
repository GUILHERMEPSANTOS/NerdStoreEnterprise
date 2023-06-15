using Core.DomainObjects.Data;
using NSE.Catalogo.API.Domain.Enitites;
using NSE.Catalogo.API.Domain.Entities;

namespace NSE.Catalogo.API.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedResult<Product>> GetPagedProducts(int pagedSize, int pagedIndex, string query);
        Task<int> GetToTalProductsWith(string query);
        Task<Product> GetById(Guid id);
        void Add(Product produto);
        void Update(Product produto);
        Task<IEnumerable<Product>> GetProducts(IEnumerable<Guid> ids);
    }
}