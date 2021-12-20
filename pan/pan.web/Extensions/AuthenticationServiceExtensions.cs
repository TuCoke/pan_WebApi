using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pan.web.Extensions
{
	public static class AuthenticationServiceExtensions
	{
		public static void Authentication(this IServiceCollection services)
		{
			services.AddAuthentication((configure) =>   // CookieAuthenticationDefaults.AuthenticationScheme
			{
				configure.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				configure.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				configure.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			}).AddCookie(configure =>
			{
				configure.LoginPath = "/login";
				configure.Cookie.Name = "banana.dorecee.auth";
				configure.Cookie.Path = "/";
				configure.Cookie.HttpOnly = true;
				//configure.Cookie.Expiration = TimeSpan.FromDays(7);
				configure.ExpireTimeSpan = TimeSpan.FromMinutes(1);
				configure.SlidingExpiration = true;
			});
		}
	}
}
