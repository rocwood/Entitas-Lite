
namespace Entitas
{
	public static class Matcher<C> where C : ContextAttribute
	{
		public static IAllOfMatcher AllOf<T1>() where T1 : IComponent
		{ return Matcher<C, T1>.All(); }
		public static IAllOfMatcher AllOf<T1, T2>() where T1 : IComponent where T2 : IComponent
		{ return Matcher<C, T1, T2>.All(); }
		public static IAllOfMatcher AllOf<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent
		{ return Matcher<C, T1, T2, T3>.All(); }
		public static IAllOfMatcher AllOf<T1, T2, T3, T4>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
		{ return Matcher<C, T1, T2, T3, T4>.All(); }
		public static IAllOfMatcher AllOf<T1, T2, T3, T4, T5>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
		{ return Matcher<C, T1, T2, T3, T4, T5>.All(); }
		public static IAllOfMatcher AllOf<T1, T2, T3, T4, T5, T6>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
		{ return Matcher<C, T1, T2, T3, T4, T5, T6>.All(); }

		public static IAnyOfMatcher AnyOf<T1>() where T1 : IComponent
		{ return Matcher<C, T1>.Any(); }
		public static IAnyOfMatcher AnyOf<T1, T2>() where T1 : IComponent where T2 : IComponent
		{ return Matcher<C, T1, T2>.Any(); }
		public static IAnyOfMatcher AnyOf<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent
		{ return Matcher<C, T1, T2, T3>.Any(); }
		public static IAnyOfMatcher AnyOf<T1, T2, T3, T4>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
		{ return Matcher<C, T1, T2, T3, T4>.Any(); }
		public static IAnyOfMatcher AnyOf<T1, T2, T3, T4, T5>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
		{ return Matcher<C, T1, T2, T3, T4, T5>.Any(); }
		public static IAnyOfMatcher AnyOf<T1, T2, T3, T4, T5, T6>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
		{ return Matcher<C, T1, T2, T3, T4, T5, T6>.Any(); }
	}

	class MatcherGeneric<C> where C : ContextAttribute
	{
		protected static int idx<T>() where T : IComponent
		{
			return ComponentIndex<C, T>.value;
		}
	}

	class Matcher<C, T1> : MatcherGeneric<C> where C : ContextAttribute where T1 : IComponent
	{
		private static IAllOfMatcher _all;
		private static IAnyOfMatcher _any;

		public static IAllOfMatcher All() { return (_all!=null)?_all:(_all=Matcher.AllOf(idx<T1>())); }
		public static IAnyOfMatcher Any() { return (_any!=null)?_any:(_any=Matcher.AnyOf(idx<T1>())); }
	}

	class Matcher<C, T1, T2> : MatcherGeneric<C> where C : ContextAttribute where T1 : IComponent where T2 : IComponent
	{
		private static IAllOfMatcher _all;
		private static IAnyOfMatcher _any;

		public static IAllOfMatcher All() { return (_all!=null)?_all:(_all=Matcher.AllOf(idx<T1>(), idx<T2>())); }
		public static IAnyOfMatcher Any() { return (_any!=null)?_any:(_any=Matcher.AnyOf(idx<T1>(), idx<T2>())); }
	}

	class Matcher<C, T1, T2, T3> : MatcherGeneric<C> where C : ContextAttribute where T1 : IComponent where T2 : IComponent where T3 : IComponent
	{
		private static IAllOfMatcher _all;
		private static IAnyOfMatcher _any;

		public static IAllOfMatcher All() { return (_all!=null)?_all:(_all=Matcher.AllOf(idx<T1>(), idx<T2>(), idx<T3>())); }
		public static IAnyOfMatcher Any() { return (_any!=null)?_any:(_any=Matcher.AnyOf(idx<T1>(), idx<T2>(), idx<T3>())); }
	}

	class Matcher<C, T1, T2, T3, T4> : MatcherGeneric<C> where C : ContextAttribute where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
	{
		private static IAllOfMatcher _all;
		private static IAnyOfMatcher _any;

		public static IAllOfMatcher All() { return (_all!=null)?_all:(_all=Matcher.AllOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>())); }
		public static IAnyOfMatcher Any() { return (_any!=null)?_any:(_any=Matcher.AnyOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>())); }
	}

	class Matcher<C, T1, T2, T3, T4, T5> : MatcherGeneric<C> where C : ContextAttribute where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
	{
		private static IAllOfMatcher _all;
		private static IAnyOfMatcher _any;

		public static IAllOfMatcher All() { return (_all!=null)?_all:(_all=Matcher.AllOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>())); }
		public static IAnyOfMatcher Any() { return (_any!=null)?_any:(_any=Matcher.AnyOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>())); }
	}

	class Matcher<C,T1,T2,T3,T4,T5,T6> : MatcherGeneric<C> where C : ContextAttribute where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
	{
		private static IAllOfMatcher _all;
		private static IAnyOfMatcher _any;

		public static IAllOfMatcher All() { return (_all!=null)?_all:(_all=Matcher.AllOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>())); }
		public static IAnyOfMatcher Any() { return (_any!=null)?_any:(_any=Matcher.AnyOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>())); }
	}
}