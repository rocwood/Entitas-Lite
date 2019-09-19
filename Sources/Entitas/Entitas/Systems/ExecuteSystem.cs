
namespace Entitas
{
	/// Execute on each entity which matches
	public abstract class ExecuteSystem : IExecuteSystem
	{
		protected IMatcher _matcher;
		protected Context _context;

		public ExecuteSystem(Context context, IMatcher matcher)
		{
			_context = context;
			_matcher = matcher;
		}

		public virtual void Execute()
		{
			var entities = _context.GetEntities(_matcher);
			foreach (var e in entities)
			{
				Execute(e);
			}
		}

		protected abstract void Execute(Entity entity);
	}
}