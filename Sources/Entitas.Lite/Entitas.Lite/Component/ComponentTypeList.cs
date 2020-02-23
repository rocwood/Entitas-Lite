using Entitas.Utils;

namespace Entitas
{
	class ComponentTypeListBase
	{
		protected static BitArray _cacheValue;

		protected static BitArray make(params int[] indices)
		{
			_cacheValue = new BitArray(ContextProvider.GetComponentCount(), indices);
			return _cacheValue;
		}

		protected static int idx<T>() where T : IComponent => ComponentIndex<T>.Get();
	}

	class ComponentTypeList<T1> : ComponentTypeListBase where T1 : IComponent
	{
		public static BitArray Get() => _cacheValue ?? make(idx<T1>());
	}

	class ComponentTypeList<T1, T2> : ComponentTypeListBase where T1 : IComponent where T2 : IComponent
	{
		public static BitArray Get() => _cacheValue ?? make(idx<T1>(), idx<T2>());
	}

	class ComponentTypeList<T1, T2, T3> : ComponentTypeListBase where T1 : IComponent where T2 : IComponent where T3 : IComponent
	{
		public static BitArray Get() => _cacheValue ?? make(idx<T1>(), idx<T2>(), idx<T3>());
	}

	class ComponentTypeList<T1, T2, T3, T4> : ComponentTypeListBase where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
	{
		public static BitArray Get() => _cacheValue ?? make(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>());
	}

	class ComponentTypeList<T1, T2, T3, T4, T5> : ComponentTypeListBase where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
	{
		public static BitArray Get() => _cacheValue ?? make(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>());
	}

	class ComponentTypeList<T1, T2, T3, T4, T5, T6> : ComponentTypeListBase where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
	{
		public static BitArray Get() => _cacheValue ?? make(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>());
	}

	class ComponentTypeList<T1, T2, T3, T4, T5, T6, T7> : ComponentTypeListBase where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
	{
		public static BitArray Get() => _cacheValue ?? make(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>(), idx<T7>());
	}
}
