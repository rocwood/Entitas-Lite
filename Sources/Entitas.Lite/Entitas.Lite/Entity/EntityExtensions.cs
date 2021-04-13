using System.Runtime.CompilerServices;

namespace Entitas
{
	public static class EntityExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Add<T>(this Entity entity) where T : IComponent
		{
			return (T)entity.AddComponent(ComponentTypeInfo<T>.index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Remove<T>(this Entity entity) where T: IComponent
		{
			entity.RemoveComponent(ComponentTypeInfo<T>.index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Has<T>(this Entity entity) where T : IComponent
		{
			return entity.HasComponent(ComponentTypeInfo<T>.index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Get<T>(this Entity entity) where T : IComponent
		{
			return (T)entity.GetComponent(ComponentTypeInfo<T>.index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Modify<T>(this Entity entity) where T : IComponent
		{
			return (T)entity.ModifyComponent(ComponentTypeInfo<T>.index);
		}
	}
}
