
namespace Entitas
{
	public class Matcher<C> where C : ContextAttribute
	{
		public static IAllOfMatcher AllOf<T1>() where T1 : IComponent
		{ return Matcher.AllOf(idx<T1>()); }
		public static IAllOfMatcher AllOf<T1, T2>() where T1 : IComponent where T2 : IComponent
		{ return Matcher.AllOf(idx<T1>(), idx<T2>()); }
		public static IAllOfMatcher AllOf<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent
		{ return Matcher.AllOf(idx<T1>(), idx<T2>(), idx<T3>()); }
		public static IAllOfMatcher AllOf<T1, T2, T3, T4>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
		{ return Matcher.AllOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>()); }
		public static IAllOfMatcher AllOf<T1, T2, T3, T4, T5>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
		{ return Matcher.AllOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>()); }
		public static IAllOfMatcher AllOf<T1, T2, T3, T4, T5, T6>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
		{ return Matcher.AllOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>()); }
		public static IAllOfMatcher AllOf<T1, T2, T3, T4, T5, T6, T7>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
		{ return Matcher.AllOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>(), idx<T7>()); }
		public static IAllOfMatcher AllOf<T1, T2, T3, T4, T5, T6, T7, T8>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent
		{ return Matcher.AllOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>(), idx<T7>(), idx<T8>()); }

		public static IAnyOfMatcher AnyOf<T1>() where T1 : IComponent
		{ return Matcher.AnyOf(idx<T1>()); }
		public static IAnyOfMatcher AnyOf<T1, T2>() where T1 : IComponent where T2 : IComponent
		{ return Matcher.AnyOf(idx<T1>(), idx<T2>()); }
		public static IAnyOfMatcher AnyOf<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent
		{ return Matcher.AnyOf(idx<T1>(), idx<T2>(), idx<T3>()); }
		public static IAnyOfMatcher AnyOf<T1, T2, T3, T4>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
		{ return Matcher.AnyOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>()); }
		public static IAnyOfMatcher AnyOf<T1, T2, T3, T4, T5>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
		{ return Matcher.AnyOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>()); }
		public static IAnyOfMatcher AnyOf<T1, T2, T3, T4, T5, T6>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
		{ return Matcher.AnyOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>()); }
		public static IAnyOfMatcher AnyOf<T1, T2, T3, T4, T5, T6, T7>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
		{ return Matcher.AnyOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>(), idx<T7>()); }
		public static IAnyOfMatcher AnyOf<T1, T2, T3, T4, T5, T6, T7, T8>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent
		{ return Matcher.AnyOf(idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>(), idx<T7>(), idx<T8>()); }

		private static int idx<T>() where T : IComponent { return ComponentIndex<C, T>.value; }
	}
}