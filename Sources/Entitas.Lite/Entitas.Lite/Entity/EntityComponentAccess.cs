using System.Runtime.CompilerServices;

namespace Entitas
{
	public partial class Entity
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T Add<T>() where T : IComponent
		{
			return (T)AddComponent(ComponentTypeInfo<T>.index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Remove<T>() where T: IComponent
		{
			RemoveComponent(ComponentTypeInfo<T>.index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Has<T>() where T : IComponent
		{
			return HasComponent(ComponentTypeInfo<T>.index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T Get<T>() where T : IComponent
		{
			return (T)GetComponent(ComponentTypeInfo<T>.index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T Modify<T>() where T : IComponent
		{
			return (T)ModifyComponent(ComponentTypeInfo<T>.index);
		}
	}
}
