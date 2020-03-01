namespace Entitas
{
	public delegate void EntityComponentChanged(Entity entity, int index, IComponent component);
	public delegate void EntityEvent(Entity entity);

	public interface IEntity
	{
		event EntityComponentChanged OnComponentAdded;
		event EntityComponentChanged OnComponentRemoved;
		event EntityEvent OnDestroyEntity;

		int totalComponents { get; }
		int creationIndex { get; }
		int id { get; }
		bool isEnabled { get; }

		ContextInfo contextInfo { get; }

		IComponent AddComponent(int index);
		void RemoveComponent(int index);
		void RemoveAllComponents();

		IComponent GetComponent(int index);
		IComponent[] GetComponents();

		bool HasComponent(int index);
		bool HasComponents(int[] indices);
		bool HasAnyComponent(int[] indices);

		void Destroy();
	}
}
