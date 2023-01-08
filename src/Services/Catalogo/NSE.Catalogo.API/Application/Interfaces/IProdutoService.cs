using NSE.Catalogo.API.Domain.Entities;

namespace NSE.Catalogo.API.Application.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(Guid id);
        void Add(Produto produto);
        void Update(Produto produto);
    }
}