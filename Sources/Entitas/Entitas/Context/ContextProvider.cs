using System;
using System.Linq;

namespace Entitas
{
	/// <summary>
	/// A static context provider
	/// </summary>
	public static class ContextProvider
	{
		public static bool useSafeAERC = true;

		public static Context Create(string name)
		{
			if (_baseContextInfo == null)
				_baseContextInfo = CollectAllComponents();

			var contextInfo = new ContextInfo(name, _baseContextInfo.componentNames, _baseContextInfo.componentTypes);

			return new Context(contextInfo);
		}
		
		public static int GetComponentCount()
		{
			if (_baseContextInfo == null)
				_baseContextInfo = CollectAllComponents();

			return _baseContextInfo.GetComponentCount();
		}

		public static int GetComponentIndex<T>() where T : IComponent => GetComponentIndex(typeof(T));
		public static int GetComponentIndex(Type type)
		{
			if (_baseContextInfo == null)
				_baseContextInfo = CollectAllComponents();

			return _baseContextInfo.GetComponentIndex(type);
		}

		public static string[] GetComponentNames()
		{
			if (_baseContextInfo == null)
				_baseContextInfo = CollectAllComponents();

			return _baseContextInfo.componentNames;
		}

		public static Type[] GetComponentTypes()
		{
			if (_baseContextInfo == null)
				_baseContextInfo = CollectAllComponents();

			return _baseContextInfo.componentTypes;
		}

		private static ContextInfo _baseContextInfo;

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
	}
}
