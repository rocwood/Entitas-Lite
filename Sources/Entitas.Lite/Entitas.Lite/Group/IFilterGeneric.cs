#if false

namespace Entitas
{
	public static class IFilterGeneric
	{
		public static T AllOf<T, T1>(this T builder) where T : IFilter where T1 : IComponent
		{
			builder?.AllOf(ComponentIndexList<T1>.Get());
			return builder;
		}
		public static T AllOf<T, T1, T2>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent
		{
			builder?.AllOf(ComponentIndexList<T1, T2>.Get());
			return builder;
		}
		public static T AllOf<T, T1, T2, T3>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent where T3 : IComponent
		{
			builder?.AllOf(ComponentIndexList<T1, T2, T3>.Get());
			return builder;
		}
		public static T AllOf<T, T1, T2, T3, T4>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
		{
			builder?.AllOf(ComponentIndexList<T1, T2, T3, T4>.Get());
			return builder;
		}
		public static T AllOf<T, T1, T2, T3, T4, T5>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
		{
			builder?.AllOf(ComponentIndexList<T1, T2, T3, T4, T5>.Get());
			return builder;
		}
		public static T AllOf<T, T1, T2, T3, T4, T5, T6>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
		{
			builder?.AllOf(ComponentIndexList<T1, T2, T3, T4, T5, T6>.Get());
			return builder;
		}
		public static T AllOf<T, T1, T2, T3, T4, T5, T6, T7>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
		{
			builder?.AllOf(ComponentIndexList<T1, T2, T3, T4, T5, T6, T7>.Get());
			return builder;
		}
		public static T AllOf<T, T1, T2, T3, T4, T5, T6, T7, T8>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent
		{
			builder?.AllOf(ComponentIndexList<T1, T2, T3, T4, T5, T6, T7, T8>.Get());
			return builder;
		}


		public static T AnyOf<T, T1>(this T builder) where T : IFilter where T1 : IComponent
		{
			builder?.AnyOf(ComponentIndexList<T1>.Get());
			return builder;
		}
		public static T AnyOf<T, T1, T2>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent
		{
			builder?.AnyOf(ComponentIndexList<T1, T2>.Get());
			return builder;
		}
		public static T AnyOf<T, T1, T2, T3>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent where T3 : IComponent
		{
			builder?.AnyOf(ComponentIndexList<T1, T2, T3>.Get());
			return builder;
		}
		public static T AnyOf<T, T1, T2, T3, T4>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
		{
			builder?.AnyOf(ComponentIndexList<T1, T2, T3, T4>.Get());
			return builder;
		}
		public static T AnyOf<T, T1, T2, T3, T4, T5>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
		{
			builder?.AnyOf(ComponentIndexList<T1, T2, T3, T4, T5>.Get());
			return builder;
		}
		public static T AnyOf<T, T1, T2, T3, T4, T5, T6>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
		{
			builder?.AnyOf(ComponentIndexList<T1, T2, T3, T4, T5, T6>.Get());
			return builder;
		}
		public static T AnyOf<T, T1, T2, T3, T4, T5, T6, T7>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
		{
			builder?.AnyOf(ComponentIndexList<T1, T2, T3, T4, T5, T6, T7>.Get());
			return builder;
		}
		public static T AnyOf<T, T1, T2, T3, T4, T5, T6, T7, T8>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent
		{
			builder?.AnyOf(ComponentIndexList<T1, T2, T3, T4, T5, T6, T7, T8>.Get());
			return builder;
		}


		public static T NoneOf<T, T1>(this T builder) where T : IFilter where T1 : IComponent
		{
			builder?.NoneOf(ComponentIndexList<T1>.Get());
			return builder;
		}
		public static T NoneOf<T, T1, T2>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent
		{
			builder?.NoneOf(ComponentIndexList<T1, T2>.Get());
			return builder;
		}
		public static T NoneOf<T, T1, T2, T3>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent where T3 : IComponent
		{
			builder?.NoneOf(ComponentIndexList<T1, T2, T3>.Get());
			return builder;
		}
		public static T NoneOf<T, T1, T2, T3, T4>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
		{
			builder?.NoneOf(ComponentIndexList<T1, T2, T3, T4>.Get());
			return builder;
		}
		public static T NoneOf<T, T1, T2, T3, T4, T5>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
		{
			builder?.NoneOf(ComponentIndexList<T1, T2, T3, T4, T5>.Get());
			return builder;
		}
		public static T NoneOf<T, T1, T2, T3, T4, T5, T6>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
		{
			builder?.NoneOf(ComponentIndexList<T1, T2, T3, T4, T5, T6>.Get());
			return builder;
		}
		public static T NoneOf<T, T1, T2, T3, T4, T5, T6, T7>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
		{
			builder?.NoneOf(ComponentIndexList<T1, T2, T3, T4, T5, T6, T7>.Get());
			return builder;
		}
		public static T NoneOf<T, T1, T2, T3, T4, T5, T6, T7, T8>(this T builder) where T : IFilter where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent
		{
			builder?.NoneOf(ComponentIndexList<T1, T2, T3, T4, T5, T6, T7, T8>.Get());
			return builder;
		}
	}
}

#endif
