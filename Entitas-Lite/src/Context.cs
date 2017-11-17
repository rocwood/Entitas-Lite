/*
 *	Entitas-Lite is a helper extension of Entitas(ECS framework for c# and Unity).
 *	Entitas-Lite focusses on easy development WITHOUT CodeGenerator of original Entitas.
 *	https://github.com/rocwood/Entitas-Lite
 */

using System;
using System.Collections.Generic;


namespace Entitas
{
	/// A context which auto-register all Components to ComponentInfoManager
	public class Context : Context<Entity>
	{
		public Context(string name, Type[] componentTypes, int startCreationIndex = 1)
			: base(componentTypes.Length, startCreationIndex, ContextInfoHelper.Make(name, componentTypes), GetAERC)
		{
			// add listener for updating lookup
			OnEntityCreated += (c, ent) => _entityLookup.Add(ent.creationIndex, (Entity)ent);
			OnEntityDestroyed += (c, ent) => _entityLookup.Remove(ent.creationIndex);
		}

		/// returns entity matching the specified creationIndex
		public Entity GetEntity(int creationIndex)
		{
			if (_entityLookup == null)
				return null;

			Entity entity = null;
			_entityLookup.TryGetValue(creationIndex, out entity);

			return entity;
		}

		private Dictionary<int, Entity> _entityLookup = new Dictionary<int, Entity>();

		private static IAERC GetAERC(IEntity entity)
		{
#if (ENTITAS_FAST_AND_UNSAFE)
            return new UnsafeAERC();
#else
			return new SafeAERC(entity);
#endif
		}
	}
}
