using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pan.Domain.Service
{
	public class AccountService : DomainServiceBase
	{
		private readonly IConfiguration _configuration;

		public AccountService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GetToken(int id, TimeSpan timeSpan)
		{
			var claims = new Claim[]{
				new Claim("UserId",id.ToString()),
			};

			var secretKey = _configuration["jwt:secretKey"];
			var audience = _configuration["jwt:audience"];
			var issuer = _configuration["jwt:issuer"];

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(issuer, audience, claims, DateTime.Now, DateTime.Now.Add(timeSpan), creds);
			var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
			return tokenStr;
		}
	}
}
