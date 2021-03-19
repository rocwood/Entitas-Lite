using System;
using System.Linq;

namespace Entitas
{
	/// <summary>
	/// A static context provider
	/// </summary>
	public static class ContextProvider
	{
		public static Context Create(string name)
		{
			if (_contextInfo == null)
				_contextInfo = CollectAllComponents();

			var contextInfo = new ContextInfo(name, _contextInfo.componentNames, _contextInfo.componentTypes);

			return new Context(name, contextInfo);
		}

		public static int GetComponentCount()
		{
			if (_contextInfo == null)
				_contextInfo = CollectAllComponents();

			return _contextInfo.GetComponentCount();
		}

		public static int GetComponentIndex<T>() where T : IComponent => GetComponentIndex(typeof(T));
		public static int GetComponentIndex(Type type)
		{
			if (_contextInfo == null)
				_contextInfo = CollectAllComponents();

			return _contextInfo.GetComponentIndex(type);
		}

		public static string[] GetComponentNames()
		{
			if (_contextInfo == null)
				_contextInfo = CollectAllComponents();

			return _contextInfo.componentNames;
		}

		public static Type[] GetComponentTypes()
		{
			if (_contextInfo == null)
				_contextInfo = CollectAllComponents();

			return _contextInfo.componentTypes;
		}

		/// Collect all public IComponent class in current domain
		private static ContextInfo CollectAllComponents()
		{
			var compType = typeof(IComponent);

			var types = AppDomain.CurrentDomain
						.GetAssemblies()
						.SelectMany(s => s.GetTypes())
						.Where(p => p.IsClass && p.IsPublic && !p.IsAbstract && compType.IsAssignableFrom(p))
						.ToArray();

			Array.Sort(types, (x, y) => string.CompareOrdinal(x.FullName, y.FullName));

			return new ContextInfo(null, types);
		}

		private static ContextInfo _contextInfo;
	}
}
