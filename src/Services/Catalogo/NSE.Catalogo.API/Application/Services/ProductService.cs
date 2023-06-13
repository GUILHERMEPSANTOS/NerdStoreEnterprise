using NSE.Catalogo.API.Application.Services.Interfaces;
using NSE.Catalogo.API.Domain.Entities;
using NSE.Catalogo.API.Domain.Interfaces;

namespace NSE.Catalogo.API.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _produtoRepository;

        public ProductService(IProductRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public void Add(Product produto)
        {
            _produtoRepository.Add(produto);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _produtoRepository.GetAll();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _produtoRepository.GetById(id);
        }

        public void Update(Product produto)
        {
            _produtoRepository.Update(produto);
        }
        public async Task<IEnumerable<Product>> GetProducts(string ids)
        {
            var wrapperIdsGuid = GetWrapperIdsGuid(ids);

            var allIdsIsValid = CheckAllValuesInWrapperIdsGuid(wrapperIdsGuid);

            if (!allIdsIsValid) return new List<Product>();

            var idsGuid = GetIdsGuid(wrapperIdsGuid);

            return await _produtoRepository.GetProducts(idsGuid);
        }

        private IEnumerable<(bool Ok, Guid Value)> GetWrapperIdsGuid(string ids)
        {
            return ids.Split(",").Select(id => (Ok: Guid.TryParse(id, out Guid idGuid), Value: idGuid));
        }

        private bool CheckAllValuesInWrapperIdsGuid(IEnumerable<(bool Ok, Guid Value)> wrapperIdsGuid)
        {
            return wrapperIdsGuid.All(wrapperIdGuid => wrapperIdGuid.Ok);
        }

        private IEnumerable<Guid> GetIdsGuid(IEnumerable<(bool Ok, Guid Value)> wrapperIdsGuid)
        {
            return wrapperIdsGuid.Select(wrapperIdGuid => wrapperIdGuid.Value);
        }
    }
}