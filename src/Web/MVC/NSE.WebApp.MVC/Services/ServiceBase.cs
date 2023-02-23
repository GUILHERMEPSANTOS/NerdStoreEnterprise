using System.Text.Json;
using Core.Communication;
using NSE.WebApp.MVC.Extensions;


namespace NSE.WebApp.MVC.Services
{
    public abstract class ServiceBase
    {
        protected bool HandleResponseError(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpRequestException(response.StatusCode);

                case 400:
                    return false;
            }

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