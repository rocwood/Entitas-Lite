/*
 *	Entitas-Lite is a helper extension of Entitas(ECS framework for c# and Unity).
 *	Entitas-Lite focusses on easy development WITHOUT CodeGenerator of original Entitas.
 *	https://github.com/rocwood/Entitas-Lite
 */

using System;
using System.Collections.Generic;


namespace Entitas
{
	/// Cache Component's indices in all Contexts
	public class ComponentIndex<T> where T : IComponent
	{
		public static int FindIn<Scope>() where Scope : ContextScope
		{
			return FindIn(Contexts.sharedInstance.GetContext<Scope>());
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
				val = c.IndexOf<T>();
				if (val < 0)
					throw new ComponentIsNotInContextException(typeof(T), c);

				_cacheLookup[c] = val;
			}

			return val;
		}

		static ComponentIndex() { }

		private static Dictionary<ContextInfo, int> _cacheLookup = new Dictionary<ContextInfo, int>();
	}

	/// Generic and fastest ComponentIndex cache with Scope + ComponentType, 
	public class ComponentIndex<Scope, T> where T : IComponent where Scope : ContextScope
	{
		public static int value
		{
			get
			{
				if (_cacheValue == -1)
					_cacheValue = ComponentIndex<T>.FindIn<Scope>();

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
