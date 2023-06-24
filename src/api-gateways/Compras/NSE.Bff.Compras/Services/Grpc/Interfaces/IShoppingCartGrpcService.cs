using NSE.Bff.Compras.DTOs;

namespace NSE.Bff.Compras.Services.Grpc.Interfaces
{
    public interface IShoppingCartGrpcService
    {
        Task<ShoppingCartDTO> GetShoppingCart();
    }
}