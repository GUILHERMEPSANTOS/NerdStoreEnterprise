using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Extensions
{
    public class CustomHttpRequestExecption : Exception
    {
        public HttpStatusCode StatusCode;
        public CustomHttpRequestExecption()
        { }
        public CustomHttpRequestExecption(string? message, Exception? innerException) : base(message, innerException)
        { }

        public CustomHttpRequestExecption(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}