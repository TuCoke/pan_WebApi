using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pan.Infrastructure.Context
{
	public class EntityChangeActionsManager<T> where T : class
	{
		private static readonly Lazy<EntityChangeActionsManager<T>> Lazy = new(() => new EntityChangeActionsManager<T>());

		internal ConcurrentDictionary<string, Action<T>> Funcs = new();

		public static EntityChangeActionsManager<T> Current => Lazy.Value;

		public void Register(string eventType, Action<T> func)
		{
			Funcs.AddOrUpdate(eventType, func, (eventType, func) => func);
		}

		public void InvokeFuncs(T entity)
		{
			foreach (var func in Funcs)
			{
				func.Value?.Invoke(entity);
			}
		}
	}
}
