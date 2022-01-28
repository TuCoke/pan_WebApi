using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace pan.web.Extensions
{
	public static class Extensions
	{
		private static readonly string _projectName = Assembly.GetEntryAssembly().GetName().Name;

		public static void AddSwagger(this IServiceCollection services)
		{
			if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development") return;
			services.AddSwaggerGen(options =>
			{
				options.CustomSchemaIds(type => type.FullName);

				typeof(Swagger.Version).GetEnumNames().ToList().ForEach(version =>
				{
					options.SwaggerDoc(version, new OpenApiInfo
					{
						Version = version,
						Title = $"{_projectName} 接口文档",
						Description = $"{_projectName} HTTP API " + version,
					});
				});

				DirectoryInfo di = new(AppContext.BaseDirectory);
				di.GetFiles("*.xml", 0).ToList().ForEach(d => { options.IncludeXmlComments(d.FullName); });

				options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
				{
					Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					BearerFormat = "JWT",
					Scheme = "Bearer"
				});

				options.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference {
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
							}
						},
						Array.Empty<string>()
					}
			});
				options.DocumentFilter<SwaggerAddEnumDescriptions>();

				options.OperationFilter<AddResponseHeadersFilter>();
				options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
				options.OperationFilter<SecurityRequirementsOperationFilter>();

			});
			services.AddSwaggerGenNewtonsoftSupport();
		}

		public static IApplicationBuilder UseSwaggerServer(this IApplicationBuilder app)
		{
			app.UseSwagger((c) =>
			{
				c.SerializeAsV2 = true;
			});
			app.UseSwaggerUI((options) =>
			{
				var projectName = Assembly.GetEntryAssembly().GetName().Name;
				typeof(Swagger.Version).GetEnumNames().OrderBy(e => e).ToList().ForEach(version =>
				{
					options.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{projectName} {version}");
					// options.RoutePrefix = string.Empty; // default prefix is swagger, generally, do not modify
				});
				options.DocExpansion(DocExpansion.None);
				options.DefaultModelsExpandDepth(-1);
			});
			return app;
		}
	}
}
