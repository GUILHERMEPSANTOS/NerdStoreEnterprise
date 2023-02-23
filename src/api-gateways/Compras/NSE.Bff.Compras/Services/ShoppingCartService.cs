using Core.Communication;
using Microsoft.Extensions.Options;
using NSE.Bff.Compras.DTOs;
using NSE.Bff.Compras.Extensions;
using NSE.Bff.Compras.Services.Interfaces;

namespace NSE.Bff.Compras.Services
{
    public class ShoppingCartService : ServiceBase, IShoppingCartService
    {
        private readonly HttpClient _httpClient;

        public ShoppingCartService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CarrinhoUrl);
        }

        public async Task<ResponseResult> AddCartItem(CartItemDTO item)
        {
            var result = await _httpClient.PostAsJsonAsync("carrinho/adicionar-item", item);
            var hasNotError = HandleResponseError(result);

            if (!hasNotError) return await DeserializeResponse<ResponseResult>(result);

            return ReturnOk();
        }

        public async Task<ShoppingCartDTO> GetShoppingCart()
        {
            var result = await _httpClient.GetAsync("/carrinho");

            HandleResponseError(result);

            return await DeserializeResponse<ShoppingCartDTO>(result);
        }

        public async Task<ResponseResult> RemoveCartItem(Guid productId)
        {
            var result = await _httpClient.DeleteAsync($"carrinho/{productId}");
            var hasNotError = HandleResponseError(result);

            if (!hasNotError) return await DeserializeResponse<ResponseResult>(result);

            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateCartItem(Guid productId, CartItemDTO item)
        {
            var result = await _httpClient.PutAsJsonAsync($"/carrinho/atualizar-item/{productId}", item);
            var hasNotError = HandleResponseError(result);

            if (!hasNotError) return await DeserializeResponse<ResponseResult>(result);

            return ReturnOk();
        }
    }
}
