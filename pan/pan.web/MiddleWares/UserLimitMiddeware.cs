using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pan.web.MiddleWares
{
    public class UserLimitMiddeware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _contexhttp;

        public UserLimitMiddeware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _contexhttp = httpContextAccessor;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string ip = _contexhttp.HttpContext.Connection.RemoteIpAddress.ToString();
            Console.WriteLine("ip的值" + ip);
            await _next(httpContext);
        }
    }
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class UserLimitMiddewareExtensions
    {
        public static IApplicationBuilder UserLimitMiddeware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserLimitMiddeware>();
        }
    }
}
