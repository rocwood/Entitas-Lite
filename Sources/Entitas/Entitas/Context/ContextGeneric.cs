
namespace Entitas
{
	public partial class Context
	{
		public IGroup AllOf<T1>() where T1 : IComponent
			=> GetGroup(MatcherCache<T1>.All());
		public IGroup AllOf<T1, T2>() where T1 : IComponent where T2 : IComponent
			=> GetGroup(MatcherCache<T1, T2>.All());
		public IGroup AllOf<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent
			=> GetGroup(MatcherCache<T1, T2, T3>.All());
		public IGroup AllOf<T1, T2, T3, T4>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
			=> GetGroup(MatcherCache<T1, T2, T3, T4>.All());
		public IGroup AllOf<T1, T2, T3, T4, T5>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
			=> GetGroup(MatcherCache<T1, T2, T3, T4, T5>.All());
		public IGroup AllOf<T1, T2, T3, T4, T5, T6>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
			=> GetGroup(MatcherCache<T1, T2, T3, T4, T5, T6>.All());

		public IGroup AnyOf<T1>() where T1 : IComponent
			=> GetGroup(MatcherCache<T1>.Any());
		public IGroup AnyOf<T1, T2>() where T1 : IComponent where T2 : IComponent
			=> GetGroup(MatcherCache<T1, T2>.Any());
		public IGroup AnyOf<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent
			=> GetGroup(MatcherCache<T1, T2, T3>.Any());
		public IGroup AnyOf<T1, T2, T3, T4>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
			=> GetGroup(MatcherCache<T1, T2, T3, T4>.Any());
		public IGroup AnyOf<T1, T2, T3, T4, T5>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent
			=> GetGroup(MatcherCache<T1, T2, T3, T4, T5>.Any());
		public IGroup AnyOf<T1, T2, T3, T4, T5, T6>() where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent
			=> GetGroup(MatcherCache<T1, T2, T3, T4, T5, T6>.Any());
	}
}
