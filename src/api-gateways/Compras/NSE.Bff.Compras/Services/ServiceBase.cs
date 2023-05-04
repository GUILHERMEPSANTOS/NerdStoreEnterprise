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

            var responseString = await ReadAsStringAsync(response);

            return JsonSerializer.Deserialize<Response>(responseString, options);
        }

        private async Task<string> ReadAsStringAsync(HttpResponseMessage response)
        {
           return await response.Content.ReadAsStringAsync();
        }

        protected async Task<ResponseResult> HandleResponse(HttpResponseMessage response)
        {
            var hasNotError = HandleResponseError(response);

            if (!hasNotError) return await DeserializeResponse<ResponseResult>(response);

            return ReturnOk();
        }

        protected ResponseResult ReturnOk()
        {
            return new ResponseResult();
        }
    }
}