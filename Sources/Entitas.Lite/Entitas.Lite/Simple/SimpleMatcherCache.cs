using System.Collections.Generic;

namespace Entitas
{
	class SimpleMatchCache<T1>  where T1 : IComponent
	{
		private static IReadOnlyList<int> indices => ComponentIndexList<T1>.Get();

		private static Matcher _allOfCached;
		private static Matcher _anyOfCached;
		public static Matcher AllOf() => _allOfCached ?? (_allOfCached = new Matcher(indices, null, null));
		public static Matcher AnyOf() => _anyOfCached ?? (_anyOfCached = new Matcher(null, indices, null));
	}
	class SimpleMatchCache<T1, T2>  where T1 : IComponent where T2 : IComponent
	{
		private static IReadOnlyList<int> indices => ComponentIndexList<T1,T2>.Get();

		private static Matcher _allOfCached;
		private static Matcher _anyOfCached;
		public static Matcher AllOf() => _allOfCached ?? (_allOfCached = new Matcher(indices, null, null));
		public static Matcher AnyOf() => _anyOfCached ?? (_anyOfCached = new Matcher(null, indices, null));
	}
	class SimpleMatchCache<T1, T2, T3>  where T1 : IComponent where T2 : IComponent where T3 : IComponent
	{
		private static IReadOnlyList<int> indices => ComponentIndexList<T1,T2,T3>.Get();

		private static Matcher _allOfCached;
		private static Matcher _anyOfCached;
		public static Matcher AllOf() => _allOfCached ?? (_allOfCached = new Matcher(indices, null, null));
		public static Matcher AnyOf() => _anyOfCached ?? (_anyOfCached = new Matcher(null, indices, null));
	}
	class SimpleMatchCache<T1, T2, T3, T4>  where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
	{
		private static IReadOnlyList<int> indices => ComponentIndexList<T1,T2,T3,T4>.Get();

		private static Matcher _allOfCached;
		private static Matcher _anyOfCached;
		public static Matcher AllOf() => _allOfCached ?? (_allOfCached = new Matcher(indices, null, null));
		public static Matcher AnyOf() => _anyOfCached ?? (_anyOfCached = new Matcher(null, indices, null));
	}
	class SimpleMatchCache<T1, T2, T3, T4, T5>  where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
	{
		private static IReadOnlyList<int> indices => ComponentIndexList<T1,T2,T3,T4,T5>.Get();

		private static Matcher _allOfCached;
		private static Matcher _anyOfCached;
		public static Matcher AllOf() => _allOfCached ?? (_allOfCached = new Matcher(indices, null, null));
		public static Matcher AnyOf() => _anyOfCached ?? (_anyOfCached = new Matcher(null, indices, null));
	}
	class SimpleMatchCache<T1, T2, T3, T4, T5, T6>  where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
	{
		private static IReadOnlyList<int> indices => ComponentIndexList<T1,T2,T3,T4,T5,T6>.Get();

		private static Matcher _allOfCached;
		private static Matcher _anyOfCached;
		public static Matcher AllOf() => _allOfCached ?? (_allOfCached = new Matcher(indices, null, null));
		public static Matcher AnyOf() => _anyOfCached ?? (_anyOfCached = new Matcher(null, indices, null));
	}
	class SimpleMatchCache<T1, T2, T3, T4, T5, T6, T7>  where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
	{
		private static IReadOnlyList<int> indices => ComponentIndexList<T1,T2,T3,T4,T5,T6,T7>.Get();

		private static Matcher _allOfCached;
		private static Matcher _anyOfCached;
		public static Matcher AllOf() => _allOfCached ?? (_allOfCached = new Matcher(indices, null, null));
		public static Matcher AnyOf() => _anyOfCached ?? (_anyOfCached = new Matcher(null, indices, null));
	}
	class SimpleMatchCache<T1, T2, T3, T4, T5, T6, T7, T8>  where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent
	{
		private static IReadOnlyList<int> indices => ComponentIndexList<T1,T2,T3,T4,T5,T6,T7,T8>.Get();

		private static Matcher _allOfCached;
		private static Matcher _anyOfCached;
		public static Matcher AllOf() => _allOfCached ?? (_allOfCached = new Matcher(indices, null, null));
		public static Matcher AnyOf() => _anyOfCached ?? (_anyOfCached = new Matcher(null, indices, null));
	}
}
