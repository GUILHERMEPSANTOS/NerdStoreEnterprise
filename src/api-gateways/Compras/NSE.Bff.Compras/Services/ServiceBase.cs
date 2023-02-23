using System.Net;
using System.Text.Json;
using Core.Communication;

namespace NSE.Bff.Compras.Services
{
    public abstract class ServiceBase
    {
        protected bool HandleResponseError(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest) return false;

            response.EnsureSuccessStatusCode();

            return true;
        }
        protected async Task<Response> DeserializeResponse<Response>(HttpResponseMessage response)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<Response>(await response.Content.ReadAsStringAsync(), options);
        }
        protected ResponseResult ReturnOk()
        {
            return new ResponseResult();
        }
    }
}