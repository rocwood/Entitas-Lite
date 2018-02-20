
namespace Entitas
{
	public static class Context<C> where C : ContextAttribute
	{
		public static Context Instance { get { return Contexts.Get<C>(); } }

		public static IGroup AllOf<T1>() where T1 : IComponent
		{ return Instance.GetGroup(Matcher<C, T1>.All()); }
		public static IGroup AllOf<T1, T2>() where T1 : IComponent where T2 : IComponent
		{ return Instance.GetGroup(Matcher<C, T1, T2>.All()); }
		public static IGroup AllOf<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent
		{ return Instance.GetGroup(Matcher<C, T1, T2, T3>.All()); }
		public static IGroup AllOf<T1, T2, T3, T4>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
		{ return Instance.GetGroup(Matcher<C, T1, T2, T3, T4>.All()); }
		public static IGroup AllOf<T1, T2, T3, T4, T5>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
		{ return Instance.GetGroup(Matcher<C, T1, T2, T3, T4, T5>.All()); }
		public static IGroup AllOf<T1, T2, T3, T4, T5, T6>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
		{ return Instance.GetGroup(Matcher<C, T1, T2, T3, T4, T5, T6>.All()); }

		public static IGroup AnyOf<T1>() where T1 : IComponent
		{ return Instance.GetGroup(Matcher<C, T1>.Any()); }
		public static IGroup AnyOf<T1, T2>() where T1 : IComponent where T2 : IComponent
		{ return Instance.GetGroup(Matcher<C, T1, T2>.Any()); }
		public static IGroup AnyOf<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent
		{ return Instance.GetGroup(Matcher<C, T1, T2, T3>.Any()); }
		public static IGroup AnyOf<T1, T2, T3, T4>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
		{ return Instance.GetGroup(Matcher<C, T1, T2, T3, T4>.Any()); }
		public static IGroup AnyOf<T1, T2, T3, T4, T5>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
		{ return Instance.GetGroup(Matcher<C, T1, T2, T3, T4, T5>.Any()); }
		public static IGroup AnyOf<T1, T2, T3, T4, T5, T6>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
		{ return Instance.GetGroup(Matcher<C, T1, T2, T3, T4, T5, T6>.Any()); }
	}
}
