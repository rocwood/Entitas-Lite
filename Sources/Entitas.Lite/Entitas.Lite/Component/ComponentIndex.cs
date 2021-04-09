using System.Runtime.CompilerServices;

namespace Entitas
{
	/// <summary>
	/// Cache Component's indices
	/// </summary>
	public static class ComponentIndex<T> where T : IComponent
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Get()
		{
			/*
			if (_cachedIndex < 0)
			{
				_cachedIndex = Context.GetComponentIndex<T>();

				if (_cachedIndex < 0)
					throw new EntitasException($"Component {typeof(T)} index is not found in Context");
			}
			*/

			return _cachedIndex;
		}

		internal static int _cachedIndex = -1;
	}
}
