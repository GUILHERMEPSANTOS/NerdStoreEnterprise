using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Extensions
{
    public class AspNetUser : IUser
    {
        public readonly IHttpContextAccessor _acessor;

        public AspNetUser(IHttpContextAccessor acessor)
        {
            _acessor = acessor;
        }

        public string Name => _acessor.HttpContext.User.Identity.Name;

        public IEnumerable<Claim> GetClaims()
        {
            return _acessor.HttpContext.User.Claims;
        }

        public HttpContext GetHttpContext() => _acessor.HttpContext;

        public string GetUserEmail()
        {
            return IsAuthenticated()
               ? _acessor.HttpContext.User.GetUserEmail()
               : "";
        }

        public Guid GetUserId()
        {
            string input = _acessor.HttpContext.User.GetUserId();

            return IsAuthenticated()
                            ? Guid.Parse(input: input)
                            : Guid.Empty;
        }

        public string GetUserToken()
        {
            string token = _acessor.HttpContext.User.GetUserToken();

            return IsAuthenticated()
                ? token
                : "";
        }

        public bool HasRole(string role)
        {
            return _acessor.HttpContext.User.IsInRole(role);
        }

        public bool IsAuthenticated()
        {
            return _acessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}