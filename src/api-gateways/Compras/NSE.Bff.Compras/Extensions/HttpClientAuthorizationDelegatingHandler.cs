using NSE.WebApi.Core.User;

namespace NSE.Bff.Compras.Extensions;

public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly IAspNetUser _aspNetUser;

    public HttpClientAuthorizationDelegatingHandler(IAspNetUser aspNetUser)
    {
        _aspNetUser = aspNetUser;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authenticationHeader = _aspNetUser.GetHttpContext().Request.Headers["Authorization"];

        if (!string.IsNullOrEmpty(authenticationHeader))
        {
            request.Headers.Add("Authorization", new List<string>() { authenticationHeader });
        }

        var token = _aspNetUser.GetUserToken();

        if (token is not null)
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
