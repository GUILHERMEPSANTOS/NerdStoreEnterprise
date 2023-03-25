using System.Net;
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

        public async Task<VoucherDTO> GetVoucherByCode(string code)
        {
            var httpResponse = await _httpClient.GetAsync($"/voucher/{code}");

            if (httpResponse.StatusCode == HttpStatusCode.NotFound) return null;

            HandleResponseError(httpResponse);

            return await DeserializeResponse<VoucherDTO>(httpResponse);
        }
    }
}