using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pan.web.Extensions
{
	public static class JwtServiceExtensions
	{
		public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
		{
			var secretKey = configuration["jwt:secretKey"];
			var audience = configuration["jwt:audience"];
			var issuer = configuration["jwt:issuer"];

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(o =>
				{
					o.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuerSigningKey = true,
						ValidIssuer = issuer,
						ValidAudience = audience,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
						ValidateIssuer = false,
						ValidateAudience = false
					};
				});
			return services;
		}
	}
}
