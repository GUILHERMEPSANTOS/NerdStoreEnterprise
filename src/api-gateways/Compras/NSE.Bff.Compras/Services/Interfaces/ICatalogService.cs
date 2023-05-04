using NSE.Bff.Compras.DTOs;

namespace NSE.Bff.Compras.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<ProductDTO>> GetAll();
        Task<ProductDTO> GetById(Guid productId);
        Task<IEnumerable<ProductDTO>> GetProducts(IEnumerable<Guid> ids);
    }
}