using System;

namespace Entitas
{
	/// <summary>
	/// Cache Component's indices
	/// </summary>
	public static class ComponentIndex<T> where T : IComponent
	{
		public static int Get()
		{
			if (_cachedIndex < 0)
			{
				_cachedIndex = Context.GetComponentIndex<T>();

				if (_cachedIndex < 0)
					throw new ComponentIndexNotFoundException(typeof(T));
			}

			return _cachedIndex;
		}

		private static int _cachedIndex = -1;
	}

	public class ComponentIndexNotFoundException : EntitasException
	{
		public ComponentIndexNotFoundException(Type type)
			: base(type + " is not found in Context", "")
		{
		}
	}
}
