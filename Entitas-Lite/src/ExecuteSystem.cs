/*
 *	Entitas-Lite is a helper extension of Entitas(ECS framework for c# and Unity).
 *	Entitas-Lite focusses on easy development WITHOUT CodeGenerator of original Entitas.
 *	https://github.com/rocwood/Entitas-Lite
 */


namespace Entitas
{
	/// Execute on each entity which matches
	public abstract class ExecuteSystem : IExecuteSystem
	{
		protected IMatcher<Entity> _matcher;
		protected Context _context;

		public ExecuteSystem(Context context, IMatcher<Entity> matcher)
		{
			_context = context;
			_matcher = matcher;
		}

		public void Execute()
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