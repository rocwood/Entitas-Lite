using System.Collections.Generic;

namespace Entitas
{
	/// <summary>
	/// A static name-context lookup, with a default context
	/// </summary>
	public static class Contexts
	{
		public const string DefaultContextName = "Default";

		public static Context Default
		{
			get
			{
				if (_defaultContext == null)
					_defaultContext = Get(DefaultContextName);

				return _defaultContext;
			}
		}

		public static Context Get(string name)
		{
			if (string.IsNullOrEmpty(name))
				return null;

			_lookup.TryGetValue(name, out var context);

			if (context == null)
			{
				context = ContextFactory.Create(name);
				_lookup[name] = context;
			}

			return context;
		}

		public static void Destroy()
		{
			foreach (var kv in _lookup)
			{
				var context = kv.Value;
				context.Reset();
			}

			_lookup.Clear();
			_defaultContext = null;
		}

		private static Context _defaultContext;
		private static readonly Dictionary<string, Context> _lookup = new Dictionary<string, Context>();

		static Contexts() { }
	}
}
