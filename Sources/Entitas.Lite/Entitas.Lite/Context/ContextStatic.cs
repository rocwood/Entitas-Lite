using System;
using System.Linq;

namespace Entitas
{
	public partial class Context
	{
		public const string DefaultContextName = "Default";

		public static Context Default
		{
			get {
				if (_defaultContext == null)
					_defaultContext = Create(DefaultContextName);
				
				return _defaultContext;
			}
		}

		public static Context Create(string name)
		{
			GetContextInfo();
			return new Context(name);
		}

		public static int GetComponentCount() => GetContextInfo().GetComponentCount();
		public static int GetComponentIndex<T>() where T : IComponent => GetComponentIndex(typeof(T));
		public static int GetComponentIndex(Type type) => GetContextInfo().GetComponentIndex(type);

		public static string[] GetComponentNames() => GetContextInfo().componentNames;
		public static Type[] GetComponentTypes() => GetContextInfo().componentTypes;

		public static ContextInfo GetContextInfo()
		{
			if (_contextInfo == null)
				_contextInfo = CollectAllComponents();

			return _contextInfo;
		}

		private static ContextInfo CollectAllComponents()
		{
			var types = AppDomain.CurrentDomain
						.GetAssemblies()
						.SelectMany(s => s.GetTypes())
						.Where(p => p.IsClass && p.IsPublic && !p.IsAbstract && typeof(IComponent).IsAssignableFrom(p))
						.ToArray();

			Array.Sort(types, (x, y) => string.CompareOrdinal(x.FullName, y.FullName));

			return new ContextInfo(types);
		}

		private static ContextInfo _contextInfo;
		private static Context _defaultContext;
	}
}
