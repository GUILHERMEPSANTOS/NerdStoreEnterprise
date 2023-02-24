using Core.Communication;
using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models.Carrinho;
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
            var hasNotError = HandleResponseError(result);

            if (!hasNotError) return await DeserializeResponse<ResponseResult>(result);

            return ReturnOk();
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
            var hasNotError = HandleResponseError(result);

            if (!hasNotError) return await DeserializeResponse<ResponseResult>(result);

            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateCartItem(Guid productId, CartItemViewModel item)
        {
            var result = await _httpClient.PutAsJsonAsync($"compras/carrinho-atualizar/{productId}", item);
            var hasNotError = HandleResponseError(result);

            if (!hasNotError) return await DeserializeResponse<ResponseResult>(result);

            return ReturnOk();
        }
    }
}