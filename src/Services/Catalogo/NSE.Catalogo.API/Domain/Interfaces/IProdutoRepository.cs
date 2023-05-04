using Core.DomainObjects.Data;
using NSE.Catalogo.API.Domain.Entities;

namespace NSE.Catalogo.API.Domain.Interfaces
{
    public interface IProdutoRepository : IRepository<Product>
    { 
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(Guid id);
        void Add(Product produto);
        void Update(Product produto);
         Task<IEnumerable<Product>> GetProducts(IEnumerable<Guid> ids);
    }
}