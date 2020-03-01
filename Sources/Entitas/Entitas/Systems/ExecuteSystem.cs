using System.Threading.Tasks;

namespace Entitas
{
	/// Execute on each entity which matches
	public abstract class ExecuteSystem : IExecuteSystem
	{
		protected IGroup _group;

		public ExecuteSystem(Context context, IMatcher matcher)
		{
			_group = context.GetGroup(matcher);
		}

		public ExecuteSystem(IGroup group)
		{
			_group = group;
		}

		public virtual void Execute()
		{
			var entities = _group.GetEntities();

			foreach (var e in entities)
			{
				Execute(e);
			}
		}

		protected abstract void Execute(Entity entity);
	}

	/// Execute parallelly on each entity which matches
	public abstract class ParallelExecuteSystem : ExecuteSystem
	{
		public ParallelExecuteSystem(Context context, IMatcher matcher) : base(context, matcher) {}

		public ParallelExecuteSystem(IGroup group) : base(group) {}

		public override void Execute()
		{
			var entities = _group.GetEntities();

			Parallel.ForEach(entities, Execute);
		}
	}
}
