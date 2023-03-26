using Core.Communication;
using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models.Carrinho;
using NSE.WebApp.MVC.Models.Pedido;
using NSE.WebApp.MVC.Services.Interfaces;

namespace NSE.WebApp.MVC.Services
{
    public class ComprasBffService : ServiceBase, IComprasBffService
    {
        private readonly HttpClient _httpClient;

        public ComprasBffService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(settings.Value.ComprasBffUrl);
        }

        public async Task<ResponseResult> AddCartItem(CartItemViewModel item)
        {
            var result = await _httpClient.PostAsJsonAsync("compras/carrinho-adicionar", item);

            return await HandleResponse(result);
        }

        public async Task<ResponseResult> ApplyVoucher(string code)
        {
            var result = await _httpClient.PostAsJsonAsync("compras/carrinho/aplicar-voucher", code);

            return await HandleResponse(result);
        }

        public async Task<ShoppingCartViewModel> GetShoppingCart()
        {
            var result = await _httpClient.GetAsync("compras/carrinho");

            HandleResponseError(result);

            return await DeserializeResponse<ShoppingCartViewModel>(result);
        }

        public async Task<int> GetShoppingCartItemsQuantity()
        {
            var result = await _httpClient.GetAsync("compras/carrinho-quantidade");

            HandleResponseError(result);

            return await DeserializeResponse<int>(result);
        }

        public async Task<ResponseResult> RemoveCartItem(Guid productId)
        {
            var result = await _httpClient.DeleteAsync($"compras/carrinho-remover/{productId}");

            return await HandleResponse(result);
        }

        public async Task<ResponseResult> UpdateCartItem(Guid productId, CartItemViewModel item)
        {
            var result = await _httpClient.PutAsJsonAsync($"compras/carrinho-atualizar/{productId}", item);

            return await HandleResponse(result);
        }
    }
}