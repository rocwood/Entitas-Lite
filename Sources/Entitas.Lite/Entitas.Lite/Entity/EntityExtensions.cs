
namespace Entitas
{
	public static class EntityExtensions
	{
		public static T Add<T>(this Entity entity) where T : IComponent
		{
			int index = ComponentIndex<T>.Get();
			return (T)entity.AddComponent(index);
		}

		public static void Remove<T>(this Entity entity) where T: IComponent
		{
			int index = ComponentIndex<T>.Get();
			entity.RemoveComponent(index);
		}
		
		public static bool Has<T>(this Entity entity) where T : IComponent
		{
			int index = ComponentIndex<T>.Get();
			return entity.HasComponent(index);
		}

		public static T Get<T>(this Entity entity) where T : IComponent
		{
			int index = ComponentIndex<T>.Get();
			return (T)entity.GetComponent(index);
		}

		public static T Modify<T>(this Entity entity) where T : IComponent
		{
			int index = ComponentIndex<T>.Get();
			return (T)entity.ModifyComponent(index);
		}
	}
}
