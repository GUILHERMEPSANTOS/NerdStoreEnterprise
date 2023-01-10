using Microsoft.AspNetCore.Http;

namespace NSE.WebApi.Core.Identidade.Filter;

public class CustomAuthorization
{
    public static bool ValidateUserClaims(HttpContext context, string claimName, string claimValue)
    {
        return (context.User.Identity?.IsAuthenticated ?? false) &&
                context.User.Claims.Any(claim => claim.Type == claimName && claim.Value.Contains(claimValue));
    }
}
