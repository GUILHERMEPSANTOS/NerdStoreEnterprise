using System.Net;
using Microsoft.Extensions.Options;
using NSE.Bff.Compras.DTOs;
using NSE.Bff.Compras.Extensions;
using NSE.Bff.Compras.Services.Interfaces;

namespace NSE.Bff.Compras.Services
{
    public class CustomerService : ServiceBase, ICustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CustomerUrl);
        }

        public async Task<AddressDTO> GetAddress()
        {
            var response = await _httpClient.GetAsync("/customers/address");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandleResponseError(response);

            return await DeserializeResponse<AddressDTO>(response);
        }
    }
}