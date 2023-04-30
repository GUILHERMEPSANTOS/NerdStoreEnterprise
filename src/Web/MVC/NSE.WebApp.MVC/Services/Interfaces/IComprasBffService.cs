using Core.Communication;
using NSE.WebApp.MVC.Models.Carrinho;
using NSE.WebApp.MVC.Models.Cliente;
using NSE.WebApp.MVC.Models.Pedido;

namespace NSE.WebApp.MVC.Services.Interfaces
{
    public interface IComprasBffService
    {
        Task<ShoppingCartViewModel> GetShoppingCart();
        Task<ResponseResult> ApplyVoucher(string code);
        Task<int> GetShoppingCartItemsQuantity();
        Task<ResponseResult> AddCartItem(CartItemViewModel item);
        Task<ResponseResult> UpdateCartItem(Guid productId, CartItemViewModel item);
        Task<ResponseResult> RemoveCartItem(Guid productId);
        TransactionViewModel MapToOrder(ShoppingCartViewModel shoppingCart, AddressViewModel address);
    }
}