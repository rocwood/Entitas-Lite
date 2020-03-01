namespace Entitas
{
	public delegate void EntityComponentChanged(IEntity entity, int index, IComponent component);
	public delegate void EntityEvent(IEntity entity);

	public interface IEntity : IAERC
	{
		event EntityComponentChanged OnComponentAdded;
		event EntityComponentChanged OnComponentRemoved;
		event EntityEvent OnEntityReleased;
		event EntityEvent OnDestroyEntity;

		int totalComponents { get; }
		int creationIndex { get; }
		bool isEnabled { get; }

		ContextInfo contextInfo { get; }
		IAERC aerc { get; }

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
