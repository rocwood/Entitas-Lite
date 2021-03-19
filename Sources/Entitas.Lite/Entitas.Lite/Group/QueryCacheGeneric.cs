using System.Collections.Generic;

namespace Entitas
{
	class QueryCacheGeneric<T1>  where T1 : IComponent
	{
		private static IReadOnlyList<int> indices => ComponentIndexList<T1>.Get();

		private static Query _allCached;
		private static Query _anyCached;
		public static Query WithAll() => _allCached ?? (_allCached = new Query(true, indices, null, null));
		public static Query WithAny() => _anyCached ?? (_anyCached = new Query(true, null, indices, null));
	}
	class QueryCacheGeneric<T1, T2>  where T1 : IComponent where T2 : IComponent
	{
		private static IReadOnlyList<int> indices => ComponentIndexList<T1,T2>.Get();

		private static Query _allCached;
		private static Query _anyCached;
		public static Query WithAll() => _allCached ?? (_allCached = new Query(true, indices, null, null));
		public static Query WithAny() => _anyCached ?? (_anyCached = new Query(true, null, indices, null));
	}
	class QueryCacheGeneric<T1, T2, T3>  where T1 : IComponent where T2 : IComponent where T3 : IComponent
	{
		private static IReadOnlyList<int> indices => ComponentIndexList<T1,T2,T3>.Get();

		private static Query _allCached;
		private static Query _anyCached;
		public static Query WithAll() => _allCached ?? (_allCached = new Query(true, indices, null, null));
		public static Query WithAny() => _anyCached ?? (_anyCached = new Query(true, null, indices, null));
	}
	class QueryCacheGeneric<T1, T2, T3, T4>  where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
	{
		private static IReadOnlyList<int> indices => ComponentIndexList<T1,T2,T3,T4>.Get();

		private static Query _allCached;
		private static Query _anyCached;
		public static Query WithAll() => _allCached ?? (_allCached = new Query(true, indices, null, null));
		public static Query WithAny() => _anyCached ?? (_anyCached = new Query(true, null, indices, null));
	}
	class QueryCacheGeneric<T1, T2, T3, T4, T5>  where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
	{
		private static IReadOnlyList<int> indices => ComponentIndexList<T1,T2,T3,T4,T5>.Get();

		private static Query _allCached;
		private static Query _anyCached;
		public static Query WithAll() => _allCached ?? (_allCached = new Query(true, indices, null, null));
		public static Query WithAny() => _anyCached ?? (_anyCached = new Query(true, null, indices, null));
	}
	class QueryCacheGeneric<T1, T2, T3, T4, T5, T6>  where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
	{
		private static IReadOnlyList<int> indices => ComponentIndexList<T1,T2,T3,T4,T5,T6>.Get();

		private static Query _allCached;
		private static Query _anyCached;
		public static Query WithAll() => _allCached ?? (_allCached = new Query(true, indices, null, null));
		public static Query WithAny() => _anyCached ?? (_anyCached = new Query(true, null, indices, null));
	}
	class QueryCacheGeneric<T1, T2, T3, T4, T5, T6, T7>  where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
	{
		private static IReadOnlyList<int> indices => ComponentIndexList<T1,T2,T3,T4,T5,T6,T7>.Get();

		private static Query _allCached;
		private static Query _anyCached;
		public static Query WithAll() => _allCached ?? (_allCached = new Query(true, indices, null, null));
		public static Query WithAny() => _anyCached ?? (_anyCached = new Query(true, null, indices, null));
	}
}
