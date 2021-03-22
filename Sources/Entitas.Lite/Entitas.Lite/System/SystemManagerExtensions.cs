using System;
using System.Linq;

namespace Entitas
{
	public static class SystemManagerExtensions
	{
		public static SystemManager Add<T>(this SystemManager manager, int priority = 0) where T : SystemBase, new()
		{
			return manager.Add(new T(), priority);
		}

		public static SystemManager Add(this SystemManager manager, Type type, int priority = 0)
		{
			if (!typeof(SystemBase).IsAssignableFrom(type))
				throw new EntitasException($"{type.FullName} is not SystemBase");

			var system = Activator.CreateInstance(type) as SystemBase;

			return manager.Add(system, priority);
		}

		public static SystemManager CollectAll(this SystemManager manager, string fullNamePrefix = null)
		{
			var types = AppDomain.CurrentDomain
						.GetAssemblies()
						.Where(s => !s.FullName.StartsWith("System.") && !s.FullName.StartsWith("Entitas."))
						.SelectMany(s => s.GetTypes())
						.Where(p => p.IsClass && p.IsPublic && !p.IsAbstract && typeof(SystemBase).IsAssignableFrom(p))
						.ToArray();

			foreach (var type in types)
			{
				if (fullNamePrefix != null && !type.FullName.StartsWith(fullNamePrefix, StringComparison.Ordinal))
					continue;

				int priority = 0;

				var attribs = type.GetCustomAttributes(typeof(SystemPriorityAttribute), false);
				if (attribs != null && attribs.Length > 0)
					priority = ((SystemPriorityAttribute)attribs[0]).priority;

				manager.Add(type, priority);
			}

			return manager;
		}
	}
}
