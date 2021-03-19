namespace Entitas
{
	public static class QueryBuilderGeneric
	{
		public static QueryBuilder WithAll<T1>(this QueryBuilder builder) where T1 : IComponent
			=> builder.WithAll(ComponentIndexList<T1>.Get());
		public static QueryBuilder WithAll<T1, T2>(this QueryBuilder builder) where T1 : IComponent where T2 : IComponent
			=> builder.WithAll(ComponentIndexList<T1, T2>.Get());
		public static QueryBuilder WithAll<T1, T2, T3>(this QueryBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent
			=> builder.WithAll(ComponentIndexList<T1, T2, T3>.Get());
		public static QueryBuilder WithAll<T1, T2, T3, T4>(this QueryBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
			=> builder.WithAll(ComponentIndexList<T1, T2, T3, T4>.Get());
		public static QueryBuilder WithAll<T1, T2, T3, T4, T5>(this QueryBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
			=> builder.WithAll(ComponentIndexList<T1, T2, T3, T4, T5>.Get());
		public static QueryBuilder WithAll<T1, T2, T3, T4, T5, T6>(this QueryBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
			=> builder.WithAll(ComponentIndexList<T1, T2, T3, T4, T5, T6>.Get());
		public static QueryBuilder WithAll<T1, T2, T3, T4, T5, T6, T7>(this QueryBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
			=> builder.WithAll(ComponentIndexList<T1, T2, T3, T4, T5, T6, T7>.Get());

		public static QueryBuilder WithAny<T1>(this QueryBuilder builder) where T1 : IComponent
			=> builder.WithAny(ComponentIndexList<T1>.Get());
		public static QueryBuilder WithAny<T1, T2>(this QueryBuilder builder) where T1 : IComponent where T2 : IComponent
			=> builder.WithAny(ComponentIndexList<T1, T2>.Get());
		public static QueryBuilder WithAny<T1, T2, T3>(this QueryBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent
			=> builder.WithAny(ComponentIndexList<T1, T2, T3>.Get());
		public static QueryBuilder WithAny<T1, T2, T3, T4>(this QueryBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
			=> builder.WithAny(ComponentIndexList<T1, T2, T3, T4>.Get());
		public static QueryBuilder WithAny<T1, T2, T3, T4, T5>(this QueryBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
			=> builder.WithAny(ComponentIndexList<T1, T2, T3, T4, T5>.Get());
		public static QueryBuilder WithAny<T1, T2, T3, T4, T5, T6>(this QueryBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
			=> builder.WithAny(ComponentIndexList<T1, T2, T3, T4, T5, T6>.Get());
		public static QueryBuilder WithAny<T1, T2, T3, T4, T5, T6, T7>(this QueryBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
			=> builder.WithAny(ComponentIndexList<T1, T2, T3, T4, T5, T6, T7>.Get());

		public static QueryBuilder WithNone<T1>(this QueryBuilder builder) where T1 : IComponent
			=> builder.WithNone(ComponentIndexList<T1>.Get());
		public static QueryBuilder WithNone<T1, T2>(this QueryBuilder builder) where T1 : IComponent where T2 : IComponent
			=> builder.WithNone(ComponentIndexList<T1, T2>.Get());
		public static QueryBuilder WithNone<T1, T2, T3>(this QueryBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent
			=> builder.WithNone(ComponentIndexList<T1, T2, T3>.Get());
		public static QueryBuilder WithNone<T1, T2, T3, T4>(this QueryBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
			=> builder.WithNone(ComponentIndexList<T1, T2, T3, T4>.Get());
		public static QueryBuilder WithNone<T1, T2, T3, T4, T5>(this QueryBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
			=> builder.WithNone(ComponentIndexList<T1, T2, T3, T4, T5>.Get());
		public static QueryBuilder WithNone<T1, T2, T3, T4, T5, T6>(this QueryBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
			=> builder.WithNone(ComponentIndexList<T1, T2, T3, T4, T5, T6>.Get());
		public static QueryBuilder WithNone<T1, T2, T3, T4, T5, T6, T7>(this QueryBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
			=> builder.WithNone(ComponentIndexList<T1, T2, T3, T4, T5, T6, T7>.Get());
	}
}
