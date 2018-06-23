using System;
using System.Collections.Generic;

namespace Entitas
{
	/// Cache Component's indices in all Contexts
	public class ComponentIndex<T> where T : IComponent
	{
		public static int FindIn<C>() where C : ContextAttribute
		{
			return FindIn(Contexts.Get<C>());
		}

		public static int FindIn(Context c)
		{
			if (c == null)
				return -1;

			return FindIn(c.contextInfo);
		}

		public static int FindIn(ContextInfo c)
		{
			if (c == null)
				return -1;

			int val = -1;
			if (!_cacheLookup.TryGetValue(c, out val))
			{
				val = c.Find<T>();
				if (val < 0)
					throw new ComponentIsNotInContextException(typeof(T), c);

				_cacheLookup[c] = val;
			}

			return val;
		}

		static ComponentIndex() { }

		private static Dictionary<ContextInfo, int> _cacheLookup = new Dictionary<ContextInfo, int>();
	}

	/// Generic and fastest ComponentIndex cache with Context + ComponentType, 
	public class ComponentIndex<C, T> where T : IComponent where C : ContextAttribute
	{
		public static int value
		{
			get
			{
				if (_cacheValue == -1)
					_cacheValue = ComponentIndex<T>.FindIn<C>();

				return _cacheValue;
			}
		}

		private static int _cacheValue = -1;
	}

	
	public class ComponentIsNotInContextException : EntitasException
	{
		public ComponentIsNotInContextException(Type type, ContextInfo contextInfo)
			: base(type + " is not in Context " + contextInfo.name, "")
		{
		}
	}
}
