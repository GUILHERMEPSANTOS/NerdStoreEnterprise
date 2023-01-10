using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NSE.WebApi.Core.Identidade.Filter
{
    public class ClaimFilterRequirement : IAuthorizationFilter
    {

        private readonly Claim claim;

        public ClaimFilterRequirement(Claim claim)
        {
            this.claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            bool IsAuthenticated = context.HttpContext.User.Identity?.IsAuthenticated ?? false;

            if (!IsAuthenticated)
            {
                HandleStatusResult(context: context, status: 401);
                return;
            }

            bool hasAuthorize = CustomAuthorization.ValidateUserClaims(context.HttpContext, claim.Type, claim.Value);

            if (!hasAuthorize)
            {
                HandleStatusResult(context: context, status: 403);
            }
        }

        private void HandleStatusResult(AuthorizationFilterContext context, int status)
        {
            context.Result = new StatusCodeResult(status);
        }
    }
}