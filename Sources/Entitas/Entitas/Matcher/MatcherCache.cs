namespace Entitas
{
	class MatcherCacheBase
	{
		protected static int idx<T>() where T:IComponent => ComponentIndex<T>.Get();
	}

	class MatcherCache<T1> : MatcherCacheBase where T1 : IComponent
	{
		private static IMatcher _all;
		private static IMatcher _any;

		public static IMatcher All() => _all??(_all=Matcher.AllOf(idx<T1>()));
		public static IMatcher Any() => _any??(_any=Matcher.AnyOf(idx<T1>()));
	}

	class MatcherCache<T1, T2> : MatcherCacheBase where T1 : IComponent where T2 : IComponent
	{
		private static IMatcher _all;
		private static IMatcher _any;

		public static IMatcher All() => _all??(_all=Matcher.AllOf(idx<T1>(), idx<T2>()));
		public static IMatcher Any() => _any??(_any=Matcher.AnyOf(idx<T1>(), idx<T2>()));
	}

	class MatcherCache<T1, T2, T3> : MatcherCacheBase where T1 : IComponent where T2 : IComponent where T3 : IComponent
	{
		private static IMatcher _all;
		private static IMatcher _any;

		public static IMatcher All() => _all??(_all=Matcher.AllOf(idx<T1>(), idx<T2>(), idx<T3>()));
		public static IMatcher Any() => _any??(_any=Matcher.AnyOf(idx<T1>(), idx<T2>(), idx<T3>()));
	}

	class MatcherCache<T1, T2, T3, T4> : MatcherCacheBase where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
	{
		private static IMatcher _all;
		private static IMatcher _any;

		public static IMatcher All() => _all??(_all=Matcher.AllOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>()));
		public static IMatcher Any() => _any??(_any=Matcher.AnyOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>()));
	}

	class MatcherCache<T1, T2, T3, T4, T5> : MatcherCacheBase where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
	{
		private static IMatcher _all;
		private static IMatcher _any;

		public static IMatcher All() => _all??(_all=Matcher.AllOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>()));
		public static IMatcher Any() => _any??(_any=Matcher.AnyOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>()));
	}

	class MatcherCache<T1, T2, T3, T4, T5, T6> : MatcherCacheBase where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
	{
		private static IMatcher _all;
		private static IMatcher _any;

		public static IMatcher All() => _all??(_all=Matcher.AllOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>()));
		public static IMatcher Any() => _any??(_any=Matcher.AnyOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>()));
	}
}
