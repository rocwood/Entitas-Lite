
using System.Runtime.CompilerServices;

namespace Entitas
{
	public static class EntityExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Add<T>(this Entity entity) where T : IComponent
		{
			int index = ComponentIndex<T>.Get();
			return (T)entity.AddComponent(index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Remove<T>(this Entity entity) where T: IComponent
		{
			int index = ComponentIndex<T>.Get();
			entity.RemoveComponent(index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Has<T>(this Entity entity) where T : IComponent
		{
			int index = ComponentIndex<T>.Get();
			return entity.HasComponent(index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Get<T>(this Entity entity) where T : IComponent
		{
			int index = ComponentIndex<T>.Get();
			return (T)entity.GetComponent(index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Modify<T>(this Entity entity) where T : IComponent
		{
			int index = ComponentIndex<T>.Get();
			return (T)entity.ModifyComponent(index);
		}
	}
}
