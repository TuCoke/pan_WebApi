using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pan.Domain.Model.Common;
using Pan.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pan.web.MiddleWares
{
    public class ExceptionMiddleware
    {
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (UserFriendlyExceptioninBase ex)
			{
				httpContext.Response.StatusCode = 200;
				httpContext.Response.ContentType = "application/json; charset=utf-8";

				var data = JsonConvert.SerializeObject(new ResponseValue()
				{
					Code = ex.Code,
					ErrorMsg = ex.ErrorMessage
				});

				_logger.LogError("用户请求发生错误：" + data);
				await httpContext.Response.WriteAsync(data); ;
			}
			//catch (Exception)
			//{
			//	httpContext.Response.StatusCode = 200;
			//	httpContext.Response.ContentType = "application/json; charset=utf-8";
			//	await httpContext.Response.WriteAsync("Exception！！");
			//}
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class ExceptionMiddlewareExtensions
	{
		public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ExceptionMiddleware>();
		}
	}
}
