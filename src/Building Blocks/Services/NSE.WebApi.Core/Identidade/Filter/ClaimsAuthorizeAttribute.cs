using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NSE.WebApi.Core.Identidade.Filter
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(ClaimFilterRequirement))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
}