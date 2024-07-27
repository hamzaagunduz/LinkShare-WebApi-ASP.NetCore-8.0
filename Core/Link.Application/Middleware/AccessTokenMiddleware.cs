using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Middleware
{
    public class AccessTokenMiddleware
    {
        private readonly RequestDelegate _next;
        public AccessTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            var accessToken = await httpContext.GetTokenAsync("access_token");
            httpContext.Items["AccessToken"] = accessToken;
            await _next(httpContext);
        }
    }
}
