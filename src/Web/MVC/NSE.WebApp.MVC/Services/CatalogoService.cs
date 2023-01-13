using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Interfaces.Catalogo;
using NSE.WebApp.MVC.Models.Catalogo;

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

        public async Task<IEnumerable<ProdutoViewModel>> GetAll()
        {
            var httpResponse = await _httpClient.GetAsync("catalogo/produtos");

            HandleResponseError(httpResponse);

            return await DeserializeResponse<IEnumerable<ProdutoViewModel>>(httpResponse);
        }

        public async Task<ProdutoViewModel> GetById(Guid id)
        {
            var httpResponse = await _httpClient.GetAsync($"catalogo/produto/{id}");

            HandleResponseError(httpResponse);

            return await DeserializeResponse<ProdutoViewModel>(httpResponse);
        }
    }
}