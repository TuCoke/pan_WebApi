using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using pan.web.Extensions;
using pan.web.MiddleWares;
using Pan.Infrastructure.Context;
using Pan.Infrastructure.UnitOfWoks;
using System;
using System.IO;
using System.Reflection;

namespace pan.web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddResponseCompression();
			services.AddHttpContextAccessor();
			services.AddHttpClient();

			services.AddMediatR(Assembly.Load("Pan.Domain"));
			services.AddAutoMapper(Assembly.Load("Pan.Domain"));

			services.AddMySqlService<EFCoreDbContext>(Configuration);
			services.AddJwt(Configuration);
			services.AddAuthentication();
			services.AddSwagger();

			services.RegistDomainService();
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
			services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
			services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
			services.RegistEntityChangeManager();

			services
				.AddControllersWithViews((options) =>
				{
					// options.Filters.Add<MyAuthorizeAttribute>();
					// options.Filters.Add(new ModelStateFilter());
				})
				.ConfigureApiBehaviorOptions(options =>
				{
					options.SuppressModelStateInvalidFilter = true;
				})
				.AddFluentValidation(options =>
				{
					//options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
					options.RegisterValidatorsFromAssembly(Assembly.Load("Pan.Domain"));
					FluentValidation.ValidatorOptions.Global.LanguageManager = new Pan.Domain.Validator.MyLanguageManager();
				})
				.AddNewtonsoftJson(options =>
				{
					options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
					options.SerializerSettings.ContractResolver = new DefaultContractResolver();
					options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
				})
				.SetCompatibilityVersion(CompatibilityVersion.Latest);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwaggerServer();
			}

			app.UseCors(policy =>
			{
				policy.SetIsOriginAllowed(origin => true)
					.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowCredentials();
			});
			app.UseStaticFiles();
			app.UseStaticFiles(new StaticFileOptions()
			{
				FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(Path.Combine(env.ContentRootPath, "wwwroot")),
				// RequestPath = new PathString("/root"), //ÐéÄâÄ¿Â¼
				OnPrepareResponse = (ctx) =>
				{
					const int cacheMinutes = 30;
					var headers = ctx.Context.Response.GetTypedHeaders();
					headers.CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
					{
						MaxAge = TimeSpan.FromMinutes(cacheMinutes)
					};
				}
			});
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseTokenParseMiddleware();
			app.UseResponseCompression();
			app.UseExceptionMiddleware();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
