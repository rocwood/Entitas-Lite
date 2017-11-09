/*
 *	Entitas-Lite is a helper extension of Entitas(ECS framework for c# and Unity).
 *	Entitas-Lite focusses on easy development WITHOUT CodeGenerator of original Entitas.
 *	https://github.com/rocwood/Entitas-Lite
 */


namespace Entitas
{
	/// API Extension for Entity, using ComponentInfo<T> to Component->Index mapping
	public static class EntityExtension
	{
		public static T AddComponent<T>(this Entity entity) where T : IComponent, new()
		{
			int index = ComponentInfo<T>.index;

			T component = entity.CreateComponent<T>(index);
			entity.AddComponent(index, component);

			return component;
		}

		public static T ReplaceComponent<T>(this Entity entity) where T : IComponent, new()
		{
			int index = ComponentInfo<T>.index;

			T component = entity.CreateComponent<T>(index);
			entity.ReplaceComponent(index, component);

			return component;
		}

		public static void RemoveComponent<T>(this Entity entity) where T: IComponent
		{
			entity.RemoveComponent(ComponentInfo<T>.index);
		}

		public static bool HasComponent<T>(this Entity entity) where T : IComponent
		{
			return entity.HasComponent(ComponentInfo<T>.index);
		}

		public static T GetComponent<T>(this Entity entity) where T : IComponent
		{
			return (T)entity.GetComponent(ComponentInfo<T>.index);
		}
	}
}
