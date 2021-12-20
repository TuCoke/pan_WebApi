using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace pan.web.MiddleWares
{
	public class TokenParseMiddleware
	{
		private readonly RequestDelegate _next;

		public TokenParseMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			string bearer = httpContext.Request.Headers["Authorization"].FirstOrDefault();
			if (!string.IsNullOrEmpty(bearer) && bearer.Contains("Bearer"))
			{
				string[] jwt = bearer.Split(' ');
				var tokenObj = new JwtSecurityToken(jwt[1]);

				var claimsIdentity = new ClaimsIdentity(tokenObj.Claims);
				var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
				httpContext.User = claimsPrincipal;
			}

			await _next(httpContext);
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class TokenParseMiddlewareExtensions
	{
		public static IApplicationBuilder UseTokenParseMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<TokenParseMiddleware>();
		}
	}
}
