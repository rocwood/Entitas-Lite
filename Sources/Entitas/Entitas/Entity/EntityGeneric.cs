
namespace Entitas
{
	/// Generic interface of Entity, using ComponentIndex to Component->Index mapping
	public partial class Entity
	{
		public T Add<T>() where T : IComponent
		{
			int index = ComponentIndex<T>.Get();
			return (T)AddComponent(index);
		}

		public void Remove<T>() where T: IComponent
		{
			int index = ComponentIndex<T>.Get();
			RemoveComponent(index);
		}
		
		public bool Has<T>() where T : IComponent
		{
			int index = ComponentIndex<T>.Get();
			return HasComponent(index);
		}

		public T Get<T>() where T : IComponent
		{
			int index = ComponentIndex<T>.Get();
			return (T)GetComponent(index);
		}

		public T Modify<T>() where T : IComponent
		{
			int index = ComponentIndex<T>.Get();
			return (T)ModifyComponent(index);
		}
	}
}
