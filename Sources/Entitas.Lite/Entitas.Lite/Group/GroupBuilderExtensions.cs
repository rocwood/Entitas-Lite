using System.Collections.Generic;

namespace Entitas
{
	public static class GroupBuilderExtensions
	{
		/*
		public static IReadOnlyList<Entity> GetEntities(this GroupBuilder builder)
		{
			var group = builder.GetGroup();
			return group.GetEntities();
		}
		*/

		public static void GetEntities(this GroupBuilder builder, IList<Entity> output)
		{
			var group = builder.GetGroup();
			group.GetEntities(output);
		}

		public static GroupBuilder WithAll<T1>(this GroupBuilder builder) where T1 : IComponent
			=> builder.WithAll(ComponentIndexList<T1>.Get());
		public static GroupBuilder WithAll<T1, T2>(this GroupBuilder builder) where T1 : IComponent where T2 : IComponent
			=> builder.WithAll(ComponentIndexList<T1, T2>.Get());
		public static GroupBuilder WithAll<T1, T2, T3>(this GroupBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent
			=> builder.WithAll(ComponentIndexList<T1, T2, T3>.Get());
		public static GroupBuilder WithAll<T1, T2, T3, T4>(this GroupBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
			=> builder.WithAll(ComponentIndexList<T1, T2, T3, T4>.Get());
		public static GroupBuilder WithAll<T1, T2, T3, T4, T5>(this GroupBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
			=> builder.WithAll(ComponentIndexList<T1, T2, T3, T4, T5>.Get());
		public static GroupBuilder WithAll<T1, T2, T3, T4, T5, T6>(this GroupBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
			=> builder.WithAll(ComponentIndexList<T1, T2, T3, T4, T5, T6>.Get());
		public static GroupBuilder WithAll<T1, T2, T3, T4, T5, T6, T7>(this GroupBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
			=> builder.WithAll(ComponentIndexList<T1, T2, T3, T4, T5, T6, T7>.Get());

		public static GroupBuilder WithAny<T1>(this GroupBuilder builder) where T1 : IComponent
			=> builder.WithAny(ComponentIndexList<T1>.Get());
		public static GroupBuilder WithAny<T1, T2>(this GroupBuilder builder) where T1 : IComponent where T2 : IComponent
			=> builder.WithAny(ComponentIndexList<T1, T2>.Get());
		public static GroupBuilder WithAny<T1, T2, T3>(this GroupBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent
			=> builder.WithAny(ComponentIndexList<T1, T2, T3>.Get());
		public static GroupBuilder WithAny<T1, T2, T3, T4>(this GroupBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
			=> builder.WithAny(ComponentIndexList<T1, T2, T3, T4>.Get());
		public static GroupBuilder WithAny<T1, T2, T3, T4, T5>(this GroupBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
			=> builder.WithAny(ComponentIndexList<T1, T2, T3, T4, T5>.Get());
		public static GroupBuilder WithAny<T1, T2, T3, T4, T5, T6>(this GroupBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
			=> builder.WithAny(ComponentIndexList<T1, T2, T3, T4, T5, T6>.Get());
		public static GroupBuilder WithAny<T1, T2, T3, T4, T5, T6, T7>(this GroupBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
			=> builder.WithAny(ComponentIndexList<T1, T2, T3, T4, T5, T6, T7>.Get());

		public static GroupBuilder WithNone<T1>(this GroupBuilder builder) where T1 : IComponent
			=> builder.WithNone(ComponentIndexList<T1>.Get());
		public static GroupBuilder WithNone<T1, T2>(this GroupBuilder builder) where T1 : IComponent where T2 : IComponent
			=> builder.WithNone(ComponentIndexList<T1, T2>.Get());
		public static GroupBuilder WithNone<T1, T2, T3>(this GroupBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent
			=> builder.WithNone(ComponentIndexList<T1, T2, T3>.Get());
		public static GroupBuilder WithNone<T1, T2, T3, T4>(this GroupBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
			=> builder.WithNone(ComponentIndexList<T1, T2, T3, T4>.Get());
		public static GroupBuilder WithNone<T1, T2, T3, T4, T5>(this GroupBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
			=> builder.WithNone(ComponentIndexList<T1, T2, T3, T4, T5>.Get());
		public static GroupBuilder WithNone<T1, T2, T3, T4, T5, T6>(this GroupBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
			=> builder.WithNone(ComponentIndexList<T1, T2, T3, T4, T5, T6>.Get());
		public static GroupBuilder WithNone<T1, T2, T3, T4, T5, T6, T7>(this GroupBuilder builder) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent
			=> builder.WithNone(ComponentIndexList<T1, T2, T3, T4, T5, T6, T7>.Get());
	}
}
