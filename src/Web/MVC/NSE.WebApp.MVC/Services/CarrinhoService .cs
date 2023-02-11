using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models.Carrinho;
using NSE.WebApp.MVC.Models.Errors;
using NSE.WebApp.MVC.Services.Interfaces;

namespace NSE.WebApp.MVC.Services
{
    public class CarrinhoService : ServiceBase, ICarrinhoService
    {
        private readonly HttpClient _httpClient;

        public CarrinhoService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(settings.Value.CarrinhoUrl);
        }

        public async Task<ResponseResult> AddCartItem(CartItemViewModel item)
        {
            var result = await _httpClient.PostAsJsonAsync("carrinho/adicionar-item", item);
            var hasNotError = HandleResponseError(result);

            if (!hasNotError) return await DeserializeResponse<ResponseResult>(result);

            return ReturnOk();
        }

        public async Task<ShoppingCartViewModel> GetShoppingCart()
        {
            var result = await _httpClient.GetAsync("/carrinho");

            HandleResponseError(result);

            return await DeserializeResponse<ShoppingCartViewModel>(result);
        }

        public async Task<ResponseResult> RemoveCartItem(Guid productId)
        {
            var result = await _httpClient.DeleteAsync("/{productId}");
            var hasNotError = HandleResponseError(result);

            if (!hasNotError) return await DeserializeResponse<ResponseResult>(result);

            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateCartItem(Guid productId, CartItemViewModel item)
        {
            var result = await _httpClient.PutAsJsonAsync("/atualizar-item/{productId}", item);
            var hasNotError = HandleResponseError(result);

            if (!hasNotError) return await DeserializeResponse<ResponseResult>(result);

            return ReturnOk();
        }
    }
}