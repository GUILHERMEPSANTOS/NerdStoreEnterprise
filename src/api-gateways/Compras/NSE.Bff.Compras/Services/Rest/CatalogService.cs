

using Microsoft.Extensions.Options;
using NSE.Bff.Compras.DTOs;
using NSE.Bff.Compras.Extensions;
using NSE.Bff.Compras.Services.Interfaces;

namespace NSE.Bff.Compras.Services
{
    public class CatalogService : ServiceBase, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CatalogoUrl);
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var httpResponse = await _httpClient.GetAsync("catalogo/produtos");

            HandleResponseError(httpResponse);

            return await DeserializeResponse<IEnumerable<ProductDTO>>(httpResponse);
        }

        public async Task<ProductDTO> GetById(Guid id)
        {
            var httpResponse = await _httpClient.GetAsync($"catalogo/produto/{id}");

            HandleResponseError(httpResponse);

            return await DeserializeResponse<ProductDTO>(httpResponse);
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts(IEnumerable<Guid> idsGuid)
        {
            var ids = string.Join(",", idsGuid);

            var httpResponse = await _httpClient.GetAsync($"catalogo/produtos/{ids}");

            HandleResponseError(httpResponse);

            return await DeserializeResponse<IEnumerable<ProductDTO>>(httpResponse);
        }
    }
}