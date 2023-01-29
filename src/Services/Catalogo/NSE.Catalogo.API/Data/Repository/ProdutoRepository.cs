using Core.Data;
using Microsoft.EntityFrameworkCore;
using NSE.Catalogo.API.Domain.Entities;
using NSE.Catalogo.API.Domain.Interfaces;

namespace NSE.Catalogo.API.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CatalogoContext _catalogoContext;

        public IUnitOfWork UnitOfWork => _catalogoContext;
        public ProdutoRepository(CatalogoContext catalogoContext)
        {
            _catalogoContext = catalogoContext;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _catalogoContext.Produtos.AsNoTracking<Product>().ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _catalogoContext.Produtos.FindAsync(id);
        }
        public void Add(Product produto)
        {
            _catalogoContext.Produtos.Add(produto);
        }

        public async void Update(Product produto)
        {
            _catalogoContext.Update(produto);
        }

        public void Dispose()
        {
            _catalogoContext?.Dispose();
        }
    }
}