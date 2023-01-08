using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _catalogoContext.Produtos.AsNoTracking<Produto>().ToListAsync();
        }

        public async Task<Produto> GetById(Guid id)
        {
            return await _catalogoContext.Produtos.FindAsync(id);
        }
        public void Add(Produto produto)
        {
            _catalogoContext.Produtos.Add(produto);
        }

        public async void Update(Produto produto)
        {
            _catalogoContext.Update(produto);
        }

        public void Dispose()
        {
            _catalogoContext?.Dispose();
        }
    }
}