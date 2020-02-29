using Entitas.Utils;

namespace Entitas
{
	class MatcherCacheBase
	{
		protected static Matcher _allCached;
		protected static Matcher _anyCached;
		protected static Matcher _noneCached;
		protected static BitArray _mask;

		public static Matcher AllOf() => _allCached ?? (_allCached = new Matcher().AllOf(_mask));
		public static Matcher AnyOf() => _anyCached ?? (_anyCached = new Matcher().AnyOf(_mask));
		public static Matcher NoneOf() => _noneCached ?? (_noneCached = new Matcher().NoneOf(_mask));
	}

	class MatcherCache<T1> : MatcherCacheBase where T1 : IComponent
	{
		static MatcherCache() => _mask = ComponentTypeList<T1>.Get();
	}

	class MatcherCache<T1, T2> : MatcherCacheBase where T1 : IComponent where T2 : IComponent
	{
		static MatcherCache() => _mask = ComponentTypeList<T1,T2>.Get();
	}

	class MatcherCache<T1, T2, T3> : MatcherCacheBase where T1 : IComponent where T2 : IComponent where T3 : IComponent
	{
		static MatcherCache() => _mask = ComponentTypeList<T1,T2,T3>.Get();
	}

	class MatcherCache<T1, T2, T3, T4> : MatcherCacheBase where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
	{
		static MatcherCache() => _mask = ComponentTypeList<T1,T2,T3,T4>.Get();
	}

	class MatcherCache<T1, T2, T3, T4, T5> : MatcherCacheBase where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
	{
		static MatcherCache() => _mask = ComponentTypeList<T1,T2,T3,T4,T5>.Get();
	}

	class MatcherCache<T1, T2, T3, T4, T5, T6> : MatcherCacheBase where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
	{
		static MatcherCache() => _mask = ComponentTypeList<T1,T2,T3,T4,T5,T6>.Get();
	}

	class MatcherCache<T1, T2, T3, T4, T5, T6, T7> : MatcherCacheBase where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
	{
		static MatcherCache() => _mask = ComponentTypeList<T1,T2,T3,T4,T5,T6,T7>.Get();
	}
}
