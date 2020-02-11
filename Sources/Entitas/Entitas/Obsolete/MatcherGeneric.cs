using System;

namespace Entitas
{
	[Obsolete("ContextAttribute is deprecated. Using Matcher.AllOf<T1,...>() and Matcher.AnyOf<T1, ...>() instead.")]
	public static class Matcher<C> where C : ContextAttribute
	{
		public static IAllOfMatcher AllOf<T1>() where T1 : IComponent
		{ return MatcherImpl<T1>.All(); }
		public static IAllOfMatcher AllOf<T1, T2>() where T1 : IComponent where T2 : IComponent
		{ return MatcherImpl<T1, T2>.All(); }
		public static IAllOfMatcher AllOf<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent
		{ return MatcherImpl<T1, T2, T3>.All(); }
		public static IAllOfMatcher AllOf<T1, T2, T3, T4>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
		{ return MatcherImpl<T1, T2, T3, T4>.All(); }
		public static IAllOfMatcher AllOf<T1, T2, T3, T4, T5>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
		{ return MatcherImpl<T1, T2, T3, T4, T5>.All(); }
		public static IAllOfMatcher AllOf<T1, T2, T3, T4, T5, T6>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
		{ return MatcherImpl<T1, T2, T3, T4, T5, T6>.All(); }

		public static IAnyOfMatcher AnyOf<T1>() where T1 : IComponent
		{ return MatcherImpl<T1>.Any(); }
		public static IAnyOfMatcher AnyOf<T1, T2>() where T1 : IComponent where T2 : IComponent
		{ return MatcherImpl<T1, T2>.Any(); }
		public static IAnyOfMatcher AnyOf<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent
		{ return MatcherImpl<T1, T2, T3>.Any(); }
		public static IAnyOfMatcher AnyOf<T1, T2, T3, T4>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
		{ return MatcherImpl<T1, T2, T3, T4>.Any(); }
		public static IAnyOfMatcher AnyOf<T1, T2, T3, T4, T5>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
		{ return MatcherImpl<T1, T2, T3, T4, T5>.Any(); }
		public static IAnyOfMatcher AnyOf<T1, T2, T3, T4, T5, T6>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
		{ return MatcherImpl<T1, T2, T3, T4, T5, T6>.Any(); }
	}
}
