using System.Net;
using Core.Communication;
using Microsoft.Extensions.Options;
using NSE.Bff.Compras.DTOs;
using NSE.Bff.Compras.Extensions;
using NSE.Bff.Compras.Services.Interfaces;

namespace NSE.Bff.Compras.Services
{
    public class OrderService : ServiceBase, IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.PedidoUrl);
        }

        public async Task<ResponseResult> FinishOrder(OrderDTO order)
        {
            var response = await _httpClient.PostAsJsonAsync("/orders/add", order);

            return await HandleResponse(response);
        }

        public async Task<OrderDTO> GetLastOrder()
        {
            var response = await _httpClient.GetAsync("/orders/last");

            if (response.StatusCode == HttpStatusCode.NotFound
                || response.StatusCode == HttpStatusCode.NoContent) return null;

            HandleResponseError(response);

            return await DeserializeResponse<OrderDTO>(response);
        }

        public async Task<IEnumerable<OrderDTO>> GetCustomers()
        {
            var response = await _httpClient.GetAsync("/orders/customer");

            if (response.StatusCode == HttpStatusCode.NotFound
                 || response.StatusCode == HttpStatusCode.NoContent) return null;

            HandleResponseError(response);

            return await DeserializeResponse<IEnumerable<OrderDTO>>(response);
        }

        public async Task<VoucherDTO> GetVoucherByCode(string code)
        {
            var httpResponse = await _httpClient.GetAsync($"/voucher/{code}");

            if (httpResponse.StatusCode == HttpStatusCode.NotFound
                || httpResponse.StatusCode == HttpStatusCode.NoContent) return null;

            HandleResponseError(httpResponse);

            return await DeserializeResponse<VoucherDTO>(httpResponse);
        }
    }
}