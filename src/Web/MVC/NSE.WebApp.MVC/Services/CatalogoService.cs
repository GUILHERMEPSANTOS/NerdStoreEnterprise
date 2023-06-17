using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Interfaces;
using NSE.WebApp.MVC.Models.Catalogo;
using NSE.WebApp.MVC.Models.Paginacao;

namespace NSE.WebApp.MVC.Services
{
    public class CatalogoService : ServiceBase, ICatalogoService
    {
        private readonly HttpClient _httpClient;

        public CatalogoService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CatalogoUrl);
        }

        public async Task<PagedResult<ProdutoViewModel>> GetProductsWithPagination(int pageIndex, int pageSize, string query)
        {
            var httpResponse = await _httpClient.GetAsync($"catalogo/produtos?PageIndex={pageIndex}&PageSize={pageSize}&Query={query}");

            HandleResponseError(httpResponse);

            return await DeserializeResponse<PagedResult<ProdutoViewModel>>(httpResponse);
        }

        public async Task<ProdutoViewModel> GetById(Guid id)
        {
            var httpResponse = await _httpClient.GetAsync($"catalogo/produto/{id}");

            HandleResponseError(httpResponse);

            return await DeserializeResponse<ProdutoViewModel>(httpResponse);
        }
    }
}