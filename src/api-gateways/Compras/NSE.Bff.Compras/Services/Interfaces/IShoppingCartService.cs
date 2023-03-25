using Core.Communication;
using NSE.Bff.Compras.DTOs;

namespace NSE.Bff.Compras.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDTO> GetShoppingCart();
        Task<ResponseResult> AddCartItem(CartItemDTO item);
        Task<ResponseResult> UpdateCartItem(Guid productId, CartItemDTO item);
        Task<ResponseResult> RemoveCartItem(Guid productId);
        Task<ResponseResult> ApplyVoucher(VoucherDTO voucher);
    }
}