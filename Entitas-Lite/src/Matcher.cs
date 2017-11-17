/*
 *	Entitas-Lite is a helper extension of Entitas(ECS framework for c# and Unity).
 *	Entitas-Lite focusses on easy development WITHOUT CodeGenerator of original Entitas.
 *	https://github.com/rocwood/Entitas-Lite
 */
 

namespace Entitas
{
	public static class Matcher
	{
		public static IAllOfMatcher<Entity> AllOf(params int[] indices) { return Matcher<Entity>.AllOf(indices); }
		public static IAllOfMatcher<Entity> AllOf(params IMatcher<Entity>[] matchers) { return Matcher<Entity>.AllOf(matchers); }

		public static IAnyOfMatcher<Entity> AnyOf(params int[] indices) { return Matcher<Entity>.AnyOf(indices); }
		public static IAnyOfMatcher<Entity> AnyOf(params IMatcher<Entity>[] matchers) { return Matcher<Entity>.AnyOf(matchers); }
	}

	public class Match<Scope> where Scope : ContextScope
	{
		public static IAllOfMatcher<Entity> AllOf<T1>() where T1 : IComponent
		{ return Matcher.AllOf(IDX<T1>()); }
		public static IAllOfMatcher<Entity> AllOf<T1, T2>() where T1 : IComponent where T2 : IComponent
		{ return Matcher.AllOf(IDX<T1>(), IDX<T2>()); }
		public static IAllOfMatcher<Entity> AllOf<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent
		{ return Matcher.AllOf(IDX<T1>(), IDX<T2>(), IDX<T3>()); }
		public static IAllOfMatcher<Entity> AllOf<T1, T2, T3, T4>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
		{ return Matcher.AllOf(IDX<T1>(), IDX<T2>(), IDX<T3>(), IDX<T4>()); }
		public static IAllOfMatcher<Entity> AllOf<T1, T2, T3, T4, T5>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
		{ return Matcher.AllOf(IDX<T1>(), IDX<T2>(), IDX<T3>(), IDX<T4>(), IDX<T5>()); }
		public static IAllOfMatcher<Entity> AllOf<T1, T2, T3, T4, T5, T6>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
		{ return Matcher.AllOf(IDX<T1>(), IDX<T2>(), IDX<T3>(), IDX<T4>(), IDX<T5>(), IDX<T6>()); }
		public static IAllOfMatcher<Entity> AllOf<T1, T2, T3, T4, T5, T6, T7>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
		{ return Matcher.AllOf(IDX<T1>(), IDX<T2>(), IDX<T3>(), IDX<T4>(), IDX<T5>(), IDX<T6>(), IDX<T7>()); }
		public static IAllOfMatcher<Entity> AllOf<T1, T2, T3, T4, T5, T6, T7, T8>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent
		{ return Matcher.AllOf(IDX<T1>(), IDX<T2>(), IDX<T3>(), IDX<T4>(), IDX<T5>(), IDX<T6>(), IDX<T7>(), IDX<T8>()); }

		public static IAnyOfMatcher<Entity> AnyOf<T1>() where T1 : IComponent
		{ return Matcher.AnyOf(IDX<T1>()); }
		public static IAnyOfMatcher<Entity> AnyOf<T1, T2>() where T1 : IComponent where T2 : IComponent
		{ return Matcher.AnyOf(IDX<T1>(), IDX<T2>()); }
		public static IAnyOfMatcher<Entity> AnyOf<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent
		{ return Matcher.AnyOf(IDX<T1>(), IDX<T2>(), IDX<T3>()); }
		public static IAnyOfMatcher<Entity> AnyOf<T1, T2, T3, T4>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
		{ return Matcher.AnyOf(IDX<T1>(), IDX<T2>(), IDX<T3>(), IDX<T4>()); }
		public static IAnyOfMatcher<Entity> AnyOf<T1, T2, T3, T4, T5>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
		{ return Matcher.AnyOf(IDX<T1>(), IDX<T2>(), IDX<T3>(), IDX<T4>(), IDX<T5>()); }
		public static IAnyOfMatcher<Entity> AnyOf<T1, T2, T3, T4, T5, T6>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
		{ return Matcher.AnyOf(IDX<T1>(), IDX<T2>(), IDX<T3>(), IDX<T4>(), IDX<T5>(), IDX<T6>()); }
		public static IAnyOfMatcher<Entity> AnyOf<T1, T2, T3, T4, T5, T6, T7>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
		{ return Matcher.AnyOf(IDX<T1>(), IDX<T2>(), IDX<T3>(), IDX<T4>(), IDX<T5>(), IDX<T6>(), IDX<T7>()); }
		public static IAnyOfMatcher<Entity> AnyOf<T1, T2, T3, T4, T5, T6, T7, T8>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent
		{ return Matcher.AnyOf(IDX<T1>(), IDX<T2>(), IDX<T3>(), IDX<T4>(), IDX<T5>(), IDX<T6>(), IDX<T7>(), IDX<T8>()); }

		private static int IDX<T>() where T : IComponent { return ComponentIndex<Scope, T>.value; }
	}


	/// Match of DefaultContext, for easy access
	public class MatchDefault : Match<DefaultContext>
	{
	}
}