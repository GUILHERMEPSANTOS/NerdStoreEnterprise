using NSE.Catalogo.API.Application.Services.Interfaces;
using NSE.Catalogo.API.Domain.Entities;
using NSE.Catalogo.API.Domain.Interfaces;

namespace NSE.Catalogo.API.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public void Add(Produto produto)
        {
            _produtoRepository.Add(produto);
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _produtoRepository.GetAll();
        }

        public async Task<Produto> GetById(Guid id)
        {
            return await _produtoRepository.GetById(id);
        }

        public void Update(Produto produto)
        {
            _produtoRepository.Update(produto);
        }
    }
}