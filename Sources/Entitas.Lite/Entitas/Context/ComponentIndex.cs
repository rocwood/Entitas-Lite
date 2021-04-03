using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Entitas
{
	/// Cache Component's indices in all Contexts
	public class ComponentIndex<T> where T : IComponent
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int FindIn<C>() where C : ContextAttribute
		{
			return FindIn(Contexts.Get<C>());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int FindIn(Context c)
		{
			if (c == null)
				return -1;

			return FindIn(c.contextInfo);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int FindIn(ContextInfo c)
		{
			if (c == null)
				return -1;

			if (c.isDefault)
			{
				// Fast lookup in default context
				if (_cachedDefaultIndex < 0)
				{
					_cachedDefaultIndex = c.Find<T>();
					if (_cachedDefaultIndex < 0)
						throw new ComponentIsNotInContextException(typeof(T), c);
				}

				return _cachedDefaultIndex;
			}
			else
			{
				int val = -1;
				if (!_cachedLookup.TryGetValue(c, out val))
				{
					val = c.Find<T>();
					if (val < 0)
						throw new ComponentIsNotInContextException(typeof(T), c);

					_cachedLookup[c] = val;
				}

				return val;
			}
		}

		private static int _cachedDefaultIndex = -1;
		private static Dictionary<ContextInfo, int> _cachedLookup = new Dictionary<ContextInfo, int>();
	}

	/// Generic and fastest ComponentIndex cache with Context + ComponentType, 
	public class ComponentIndex<C, T> where T : IComponent where C : ContextAttribute
	{
		public static int value
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (_cachedIndex < 0)
					_cachedIndex = ComponentIndex<T>.FindIn<C>();

				return _cachedIndex;
			}
		}

		private static int _cachedIndex = -1;
	}

	
	public class ComponentIsNotInContextException : EntitasException
	{
		public ComponentIsNotInContextException(Type type, ContextInfo contextInfo)
			: base(type + " is not in Context " + contextInfo.name, "")
		{
		}
	}
}
