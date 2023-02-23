using Core.Communication;
using NSE.WebApp.MVC.Models.Carrinho;

namespace NSE.WebApp.MVC.Services.Interfaces
{
    public interface ICarrinhoService
    {
        Task<ShoppingCartViewModel> GetShoppingCart();
        Task<ResponseResult> AddCartItem(CartItemViewModel item);
        Task<ResponseResult> UpdateCartItem(Guid productId, CartItemViewModel item);
        Task<ResponseResult> RemoveCartItem(Guid productId);
    }
}