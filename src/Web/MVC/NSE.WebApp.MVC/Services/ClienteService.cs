using Core.Communication;
using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models.Cliente;
using NSE.WebApp.MVC.Services.Interfaces;

namespace NSE.WebApp.MVC.Services
{
    public class ClienteService : ServiceBase, IClienteService
    {
        private readonly HttpClient _httpCLient;

        public ClienteService(HttpClient httpCLient, IOptions<AppSettings> settings)
        {
            _httpCLient = httpCLient;
            _httpCLient.BaseAddress = new Uri(settings.Value.ClienteUrl);
        }

        public async Task<ResponseResult> AddAddress(AddressViewModel address)
        {
            var httpResponse = await _httpCLient.PostAsJsonAsync("customers/address", address);

            return await HandleResponse(httpResponse);
        }

        public async Task<AddressViewModel> GetAddress()
        {
            var httpResponse = await _httpCLient.GetAsync("customers/address");

            if(httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound) return null;

           return await DeserializeResponse<AddressViewModel>(httpResponse);
        }
    }
}