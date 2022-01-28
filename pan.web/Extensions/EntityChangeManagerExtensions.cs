using Microsoft.Extensions.DependencyInjection;
using Pan.Domain.Caches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace pan.web.Extensions
{
    public static class EntityChangeManagerExtensions
    {
        public static void RegistEntityChangeManager(this IServiceCollection services)
        {
            var types = Assembly.Load("Pan.Domain").GetTypes();
            var iManagedCacheTag = typeof(IManagedCache);
            var provider = services.BuildServiceProvider();
            foreach (Type item in types.Where(t => t.IsClass && !t.IsAbstract && iManagedCacheTag.IsAssignableFrom(t)))
            {
                var cacheService = provider.GetService(item);
                item.GetMethod("ListenEntity").Invoke(cacheService, null);
            }
        }
    }
}
