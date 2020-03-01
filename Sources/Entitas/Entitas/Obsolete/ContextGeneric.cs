using System;

namespace Entitas
{
	[Obsolete("ContextAttribute is deprecated. Using Context.AllOf<T1,...>() and Context.AnyOf<T1, ...>() instead.")]
	public static class Context<C> where C : ContextAttribute
	{
		private static Context Instance => Contexts.Get(ContextAttribute.GetName<C>());

		public static IGroup AllOf<T1>() where T1 : IComponent
			=> Instance.AllOf<T1>();
		public static IGroup AllOf<T1, T2>() where T1 : IComponent where T2 : IComponent
			=> Instance.AllOf<T1, T2>();
		public static IGroup AllOf<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent
			=> Instance.AllOf<T1, T2, T3>();
		public static IGroup AllOf<T1, T2, T3, T4>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
			=> Instance.AllOf<T1, T2, T3, T4>();
		public static IGroup AllOf<T1, T2, T3, T4, T5>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
			=> Instance.AllOf<T1, T2, T3, T4, T5>();
		public static IGroup AllOf<T1, T2, T3, T4, T5, T6>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
			=> Instance.AllOf<T1, T2, T3, T4, T5, T6>();

		public static IGroup AnyOf<T1>() where T1 : IComponent
			=> Instance.AnyOf<T1>();
		public static IGroup AnyOf<T1, T2>() where T1 : IComponent where T2 : IComponent
			=> Instance.AnyOf<T1, T2>();
		public static IGroup AnyOf<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent
			=> Instance.AnyOf<T1, T2, T3>();
		public static IGroup AnyOf<T1, T2, T3, T4>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
			=> Instance.AnyOf<T1, T2, T3, T4>();
		public static IGroup AnyOf<T1, T2, T3, T4, T5>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
			=> Instance.AnyOf<T1, T2, T3, T4, T5>();
		public static IGroup AnyOf<T1, T2, T3, T4, T5, T6>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
			=> Instance.AnyOf<T1, T2, T3, T4, T5, T6>();
	}
}
