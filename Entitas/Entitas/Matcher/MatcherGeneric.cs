
namespace Entitas
{
	public class Matcher<C> where C : ContextAttribute
	{
		public static IAllOfMatcher AllOf<T1>() where T1 : IComponent
		{ return AllOf(ref a1, idx<T1>()); }
		public static IAllOfMatcher AllOf<T1, T2>() where T1 : IComponent where T2 : IComponent
		{ return AllOf(ref a2, idx<T1>(), idx<T2>()); }
		public static IAllOfMatcher AllOf<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent
		{ return AllOf(ref a3, idx<T1>(), idx<T2>(), idx<T3>()); }
		public static IAllOfMatcher AllOf<T1, T2, T3, T4>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
		{ return AllOf(ref a4, idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>()); }
		public static IAllOfMatcher AllOf<T1, T2, T3, T4, T5>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
		{ return AllOf(ref a5, idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>()); }
		public static IAllOfMatcher AllOf<T1, T2, T3, T4, T5, T6>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
		{ return AllOf(ref a6, idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>()); }
		public static IAllOfMatcher AllOf<T1, T2, T3, T4, T5, T6, T7>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
		{ return AllOf(ref a7, idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>(), idx<T7>()); }
		public static IAllOfMatcher AllOf<T1, T2, T3, T4, T5, T6, T7, T8>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent
		{ return AllOf(ref a8, idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>(), idx<T7>(), idx<T8>()); }

		public static IAnyOfMatcher AnyOf<T1>() where T1 : IComponent
		{ return AnyOf(ref o1, idx<T1>()); }
		public static IAnyOfMatcher AnyOf<T1, T2>() where T1 : IComponent where T2 : IComponent
		{ return AnyOf(ref o2, idx<T1>(), idx<T2>()); }
		public static IAnyOfMatcher AnyOf<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent
		{ return AnyOf(ref o3, idx<T1>(), idx<T2>(), idx<T3>()); }
		public static IAnyOfMatcher AnyOf<T1, T2, T3, T4>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
		{ return AnyOf(ref o4, idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>()); }
		public static IAnyOfMatcher AnyOf<T1, T2, T3, T4, T5>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
		{ return AnyOf(ref o5, idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>()); }
		public static IAnyOfMatcher AnyOf<T1, T2, T3, T4, T5, T6>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
		{ return AnyOf(ref o6, idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>()); }
		public static IAnyOfMatcher AnyOf<T1, T2, T3, T4, T5, T6, T7>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
		{ return AnyOf(ref o7, idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>(), idx<T7>()); }
		public static IAnyOfMatcher AnyOf<T1, T2, T3, T4, T5, T6, T7, T8>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent
		{ return AnyOf(ref o8, idx<T1>(), idx<T2>(), idx<T3>(), idx<T4>(), idx<T5>(), idx<T6>(), idx<T7>(), idx<T8>()); }


		private static IAllOfMatcher a1, a2, a3, a4, a5, a6, a7, a8;
		private static IAnyOfMatcher o1, o2, o3, o4, o5, o6, o7, o8;

		private static IAllOfMatcher AllOf(ref IAllOfMatcher matcher, params int[] indices)
		{
			if (matcher == null)
				matcher = Matcher.AllOf(indices);
			return matcher;
		}

		private static IAnyOfMatcher AnyOf(ref IAnyOfMatcher matcher, params int[] indices)
		{
			if (matcher == null)
				matcher = Matcher.AnyOf(indices);
			return matcher;
		}

		private static int idx<T>() where T : IComponent
		{
			return ComponentIndex<C, T>.value;
		}
	}
}