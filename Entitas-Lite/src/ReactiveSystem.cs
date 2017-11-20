/*
 *	Entitas-Lite is a helper extension of Entitas(ECS framework for c# and Unity).
 *	Entitas-Lite focusses on easy development WITHOUT CodeGenerator of original Entitas.
 *	https://github.com/rocwood/Entitas-Lite
 */

 using System;


namespace Entitas
{
	public abstract class ReactiveSystem : ReactiveSystem<Entity>
	{
		public ReactiveSystem(ICollector<Entity> collector) : base(collector)
		{
		}

		protected override ICollector<Entity> GetTrigger(IContext<Entity> context)
		{
			return null;
		}

		protected override bool Filter(Entity entity)
		{
			return true;
		}
	}
}
