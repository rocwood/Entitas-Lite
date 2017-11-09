/*
 *	Entitas-Lite is a helper extension of Entitas(ECS framework for c# and Unity).
 *	Entitas-Lite focusses on easy development WITHOUT CodeGenerator of original Entitas.
 *	https://github.com/rocwood/Entitas-Lite
 */

using System;


namespace Entitas
{
	// A context which auto-register all Components to ComponentInfoManager
	public class Context : Context<Entity>
	{
		public Context(string name, Type[] componentTypes, int startCreationIndex = 1)
			: base(componentTypes.Length, startCreationIndex, MakeContextInfo(name, componentTypes), GetAERC)
		{
			RegisterComponents(this, componentTypes);
		}

		/// returns entity matching the specified creationIndex
		public Entity GetEntityByCreationIndex(int creationIndex)
		{
			// TODO: need optimize
			var entities = GetEntities();

			int count = entities.Length;
			for (int i = 0; i < count; i++)
			{
				var entity = entities[i];
				if (entity.creationIndex == creationIndex)
					return entity;
			}

			return null;
		}

		private static ContextInfo MakeContextInfo(string name, Type[] componentTypes)
		{
			int count = componentTypes.Length;
			string[] componentNames = new string[count];

			for (int i = 0; i < count; i++)
			{
				componentNames[i] = componentTypes[i].Name;
			}

			return new ContextInfo(name, componentNames, componentTypes);
		}

		private static void RegisterComponents(Context c, Type[] componentTypes)
		{
			int count = componentTypes.Length;
			for (int i = 0; i < count; i++)
			{
				ComponentInfoManager.RegisterComponent(componentTypes[i], c, i);
			}
		}

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
