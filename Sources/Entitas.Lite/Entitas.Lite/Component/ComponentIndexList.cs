using System.Collections.Generic;

namespace Entitas
{
	class ComponentIndexListBase
	{
		protected static IReadOnlyList<int> Make(params int[] indices)
		{
			/*
			var list = new List<int>(indices.Length);
			list.AddDistinctSorted(indices);
			return list;
			*/

			return indices;
		}

		protected static int idx<T>() where T : IComponent => ComponentTypeInfo<T>.index;
	}

	class ComponentIndexList<T1> : ComponentIndexListBase where T1 : IComponent
	{
		private static IReadOnlyList<int> _cacheList;
		public static IReadOnlyList<int> Get() => _cacheList ?? (_cacheList = Make(idx<T1>()));
	}
	class ComponentIndexList<T1, T2> : ComponentIndexListBase where T1 : IComponent where T2 : IComponent
	{
		private static IReadOnlyList<int> _cacheList;
		public static IReadOnlyList<int> Get() => _cacheList ?? (_cacheList = Make(idx<T1>(), idx<T2>()));
	}
	class ComponentIndexList<T1, T2, T3> : ComponentIndexListBase where T1 : IComponent where T2 : IComponent where T3 : IComponent
	{
		private static IReadOnlyList<int> _cacheList;
		public static IReadOnlyList<int> Get() => _cacheList ?? (_cacheList = Make(idx<T1>(), idx<T2>(), idx<T3>()));
	}
	class ComponentIndexList<T1, T2, T3, T4> : ComponentIndexListBase where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
	{
		private static IReadOnlyList<int> _cacheList;
		public static IReadOnlyList<int> Get() => _cacheList ?? (_cacheList = Make(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>()));
	}
	class ComponentIndexList<T1, T2, T3, T4, T5> : ComponentIndexListBase where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
	{
		private static IReadOnlyList<int> _cacheList;
		public static IReadOnlyList<int> Get() => _cacheList ?? (_cacheList = Make(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>()));
	}
	class ComponentIndexList<T1, T2, T3, T4, T5, T6> : ComponentIndexListBase where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
	{
		private static IReadOnlyList<int> _cacheList;
		public static IReadOnlyList<int> Get() => _cacheList ?? (_cacheList = Make(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>()));
	}
	class ComponentIndexList<T1, T2, T3, T4, T5, T6, T7> : ComponentIndexListBase where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
	{
		private static IReadOnlyList<int> _cacheList;
		public static IReadOnlyList<int> Get() => _cacheList ?? (_cacheList = Make(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>(), idx<T7>()));
	}
}
