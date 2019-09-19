using System;

namespace Entitas
{
	/// <summary>
	/// Cache Component's indices
	/// </summary>
	public static class ComponentIndex<T> where T : IComponent
	{
		public static int value
		{
			get
			{
				if (_cachedIndex < 0)
				{
					_cachedIndex = ContextFactory.GetComponentIndex<T>();

					if (_cachedIndex < 0)
						throw new ComponentIndexNotFoundException(typeof(T));
				}

				return _cachedIndex;
			}
		}

		static ComponentIndex() { }

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
