/*
 *	Entitas-Lite is a helper extension of Entitas(ECS framework for c# and Unity).
 *	Entitas-Lite focusses on easy development WITHOUT CodeGenerator of original Entitas.
 *	https://github.com/rocwood/Entitas-Lite
 */


namespace Entitas
{
	/// Extension for Entity, using ComponentIndex<T> to Component->Index mapping
	public static class EntityExtension
	{
		public static T AddComponent<T>(this Entity entity) where T : IComponent, new()
		{
			int index = ComponentIndex<T>.FindIn(entity.contextInfo);

			T component = entity.CreateComponent<T>(index);
			entity.AddComponent(index, component);

			return component;
		}

		/// replace Component with a NEW one, add if not existed
		public static T ReplaceNewComponent<T>(this Entity entity) where T : IComponent, new()
		{
			int index = ComponentIndex<T>.FindIn(entity.contextInfo);

			T component = entity.CreateComponent<T>(index);
			entity.ReplaceComponent(index, component);

			return component;
		}

		public static void RemoveComponent<T>(this Entity entity) where T: IComponent
		{
			int index = ComponentIndex<T>.FindIn(entity.contextInfo);
			entity.RemoveComponent(index);
		}

		public static bool HasComponent<T>(this Entity entity) where T : IComponent
		{
			int index = ComponentIndex<T>.FindIn(entity.contextInfo);
			return entity.HasComponent(index);
		}

		public static T GetComponent<T>(this Entity entity) where T : IComponent
		{
			int index = ComponentIndex<T>.FindIn(entity.contextInfo);
			return (T)entity.GetComponent(index);
		}

		/// shorter version of GetComponent<T>
		public static T Get<T>(this Entity entity) where T : IComponent
		{
			int index = ComponentIndex<T>.FindIn(entity.contextInfo);
			return (T)entity.GetComponent(index);
		}

		/// Mark component-updated, trigger GroupEvent and ReactiveSystem
		public static void MarkUpdated<T>(this Entity entity) where T : IComponent, new()
		{
			int index = ComponentIndex<T>.FindIn(entity.contextInfo);
			entity.ReplaceComponent(index, entity.GetComponent(index));
		}
	}
}
