using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pan.Infrastructure.Context;
using Pan.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pan.web.Extensions
{
	public static class EfCoreServiceExtensions
	{
		private static readonly ILoggerFactory DbLoggerFactory = LoggerFactory.Create(builder =>
		{
			builder.AddFilter((c, level) => c == DbLoggerCategory.Database.Command.Name && level == LogLevel.Debug).AddConsole();
		});


		public static IServiceCollection AddMySqlService<T>(this IServiceCollection services, IConfiguration configuration) where T : EFCoreDbContext
		{
			services.AddDbContext<T>(options =>
			{
				options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

				var connectionString = configuration["mysql"];
				var serverVersion = new MySqlServerVersion(new Version(5, 7, 34));
				options.UseMySql(connectionString, serverVersion, mysqlOptions =>
				{
					mysqlOptions.MigrationsAssembly(typeof(FileStorage).Assembly.GetName().Name);

					mysqlOptions.EnableRetryOnFailure(
								   maxRetryCount: 3,
								   maxRetryDelay: TimeSpan.FromSeconds(10),
								   errorNumbersToAdd: new int[] { 2 });
					mysqlOptions.CommandTimeout(60);
				});

				options.UseLoggerFactory(DbLoggerFactory);
			});

			return services;
		}
	}
}
