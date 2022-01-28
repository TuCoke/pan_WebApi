using Microsoft.Extensions.DependencyInjection;
using Pan.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace pan.web.Extensions
{
	public static class ServiceRegistExtensions
	{
		public static void RegistDomainService(this IServiceCollection services)
		{
			var types = Assembly.Load("Pan.Domain").GetTypes()
				.Union(Assembly.Load("Pan.Infrastructure").GetTypes());

			foreach (Type item in types.Where(t => t.IsClass && !t.IsAbstract))
			{
				if (typeof(IScopedService).IsAssignableFrom(item))
				{
					services.AddScoped(item);
				}
				else if (typeof(ISingletonService).IsAssignableFrom(item))
				{
					services.AddSingleton(item);
				}
				else if (typeof(ITransientService).IsAssignableFrom(item))
				{
					services.AddTransient(item);
				}
			}
		}
	}
}
