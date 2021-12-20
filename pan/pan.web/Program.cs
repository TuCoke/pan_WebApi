using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace pan.web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Information()
				//.MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
				.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
				//.MinimumLevel.Override("Microsoft.AspNetCore.Mvc", Serilog.Events.LogEventLevel.Information)
				//.ReadFrom.Configuration(Configuration) // 从配置文件读取
				.Enrich.FromLogContext()
				.WriteTo.Console()
				.WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", ".log"), rollingInterval: RollingInterval.Day)
				.CreateLogger();
			Log.Information("application start");

			try
			{
				CreateHostBuilder(args).Build().Run();
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "host terminated unexpectedly");
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			var hostBuilder = Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(builder =>
				{
					builder.UseUrls("http://*:5000");
					builder.UseStartup<Startup>();
				})
				.UseSerilog();

			return hostBuilder;
		}
	}
}
