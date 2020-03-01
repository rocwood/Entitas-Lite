using System;

namespace Entitas
{
	class MatcherCacheBase
	{
		protected static IMatcher _allCached;
		protected static IMatcher _anyCached;
		protected static int[] _indices;

		public static IMatcher AllOf() => _allCached ?? (_allCached = Matcher.AllOf(_indices));
		public static IMatcher AnyOf() => _anyCached ?? (_anyCached = Matcher.AnyOf(_indices));

		protected static int idx<T>() where T : IComponent => ComponentIndex<T>.Get();

		protected static int[] sortIndices(params int[] indices)
		{
			Array.Sort(indices);
			return indices;
		}
	}

	class MatcherCache<T1> : MatcherCacheBase where T1 : IComponent
	{
		static MatcherCache() => _indices = sortIndices(idx<T1>());
	}

	class MatcherCache<T1, T2> : MatcherCacheBase where T1 : IComponent where T2 : IComponent
	{
		static MatcherCache() => _indices = sortIndices(idx<T1>(), idx<T2>());
	}

	class MatcherCache<T1, T2, T3> : MatcherCacheBase where T1 : IComponent where T2 : IComponent where T3 : IComponent
	{
		static MatcherCache() => _indices = sortIndices(idx<T1>(), idx<T2>(), idx<T3>());
	}

	class MatcherCache<T1, T2, T3, T4> : MatcherCacheBase where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
	{
		static MatcherCache() => _indices = sortIndices(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>());
	}

	class MatcherCache<T1, T2, T3, T4, T5> : MatcherCacheBase where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
	{
		static MatcherCache() => _indices = sortIndices(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>());
	}

	class MatcherCache<T1, T2, T3, T4, T5, T6> : MatcherCacheBase where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
	{
		static MatcherCache() => _indices = sortIndices(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>());
	}
}
