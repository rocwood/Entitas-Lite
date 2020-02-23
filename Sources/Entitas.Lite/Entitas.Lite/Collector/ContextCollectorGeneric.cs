namespace Entitas
{
	public static class ContextCollectorGeneric
	{
		public static Collector AllOf<T1>(this Context c) where T1 : IComponent
			=> c.CreateCollector().AllOf(ComponentTypeList<T1>.Get());
		public static Collector AllOf<T1, T2>(this Context c) where T1 : IComponent where T2 : IComponent
			=> c.CreateCollector().AllOf(ComponentTypeList<T1, T2>.Get());
		public static Collector AllOf<T1, T2, T3>(this Context c) where T1 : IComponent where T2 : IComponent where T3 : IComponent
			=> c.CreateCollector().AllOf(ComponentTypeList<T1, T2, T3>.Get());
		public static Collector AllOf<T1, T2, T3, T4>(this Context c) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
			=> c.CreateCollector().AllOf(ComponentTypeList<T1, T2, T3, T4>.Get());
		public static Collector AllOf<T1, T2, T3, T4, T5>(this Context c) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
			=> c.CreateCollector().AllOf(ComponentTypeList<T1, T2, T3, T4, T5>.Get());
		public static Collector AllOf<T1, T2, T3, T4, T5, T6>(this Context c) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
			=> c.CreateCollector().AllOf(ComponentTypeList<T1, T2, T3, T4, T5, T6>.Get());
		public static Collector AllOf<T1, T2, T3, T4, T5, T6, T7>(this Context c) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
			=> c.CreateCollector().AllOf(ComponentTypeList<T1, T2, T3, T4, T5, T6, T7>.Get());

		public static Collector AnyOf<T1>(this Context c) where T1 : IComponent
			=> c.CreateCollector().AnyOf(ComponentTypeList<T1>.Get());
		public static Collector AnyOf<T1, T2>(this Context c) where T1 : IComponent where T2 : IComponent
			=> c.CreateCollector().AnyOf(ComponentTypeList<T1, T2>.Get());
		public static Collector AnyOf<T1, T2, T3>(this Context c) where T1 : IComponent where T2 : IComponent where T3 : IComponent
			=> c.CreateCollector().AnyOf(ComponentTypeList<T1, T2, T3>.Get());
		public static Collector AnyOf<T1, T2, T3, T4>(this Context c) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
			=> c.CreateCollector().AnyOf(ComponentTypeList<T1, T2, T3, T4>.Get());
		public static Collector AnyOf<T1, T2, T3, T4, T5>(this Context c) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
			=> c.CreateCollector().AnyOf(ComponentTypeList<T1, T2, T3, T4, T5>.Get());
		public static Collector AnyOf<T1, T2, T3, T4, T5, T6>(this Context c) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
			=> c.CreateCollector().AnyOf(ComponentTypeList<T1, T2, T3, T4, T5, T6>.Get());
		public static Collector AnyOf<T1, T2, T3, T4, T5, T6, T7>(this Context c) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
			=> c.CreateCollector().AnyOf(ComponentTypeList<T1, T2, T3, T4, T5, T6, T7>.Get());

		public static Collector NoneOf<T1>(this Context c) where T1 : IComponent
			=> c.CreateCollector().NoneOf(ComponentTypeList<T1>.Get());
		public static Collector NoneOf<T1, T2>(this Context c) where T1 : IComponent where T2 : IComponent
			=> c.CreateCollector().NoneOf(ComponentTypeList<T1, T2>.Get());
		public static Collector NoneOf<T1, T2, T3>(this Context c) where T1 : IComponent where T2 : IComponent where T3 : IComponent
			=> c.CreateCollector().NoneOf(ComponentTypeList<T1, T2, T3>.Get());
		public static Collector NoneOf<T1, T2, T3, T4>(this Context c) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
			=> c.CreateCollector().NoneOf(ComponentTypeList<T1, T2, T3, T4>.Get());
		public static Collector NoneOf<T1, T2, T3, T4, T5>(this Context c) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
			=> c.CreateCollector().NoneOf(ComponentTypeList<T1, T2, T3, T4, T5>.Get());
		public static Collector NoneOf<T1, T2, T3, T4, T5, T6>(this Context c) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
			=> c.CreateCollector().NoneOf(ComponentTypeList<T1, T2, T3, T4, T5, T6>.Get());
		public static Collector NoneOf<T1, T2, T3, T4, T5, T6, T7>(this Context c) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
			=> c.CreateCollector().NoneOf(ComponentTypeList<T1, T2, T3, T4, T5, T6, T7>.Get());
	}
}
