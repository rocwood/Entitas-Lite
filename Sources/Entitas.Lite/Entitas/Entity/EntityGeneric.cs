using System.Runtime.CompilerServices;

namespace Entitas
{
	/// using ComponentIndex<T> to Component->Index mapping
	public partial class Entity
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		/// add a new Component, return the old one if exists
		public T Add<T>(bool useExisted = true) where T : IComponent, new()
		{
			T component;

			int index = ComponentIndex<T>.FindIn(_contextInfo);

			if (useExisted && HasComponent(index))
			{
				component = (T)GetComponent(index);
				FireModifiedEvent(index, component);
			}
			else
			{
				component = CreateComponent<T>(index);
				AddComponent(index, component);
			}

			return component;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T AddComponent<T>(bool useExisted = true) where T : IComponent, new()
		{
			return Add<T>(useExisted);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]      
		/// replace Component with a NEW one
		public T ReplaceNew<T>() where T : IComponent, new()
		{
			int index = ComponentIndex<T>.FindIn(_contextInfo);

			T component = CreateComponent<T>(index);
			ReplaceComponent(index, component);

			return component;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T ReplaceNewComponent<T>() where T : IComponent, new()
		{
			return ReplaceNew<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Remove<T>(bool ignoreNotFound = true) where T: IComponent
		{
			int index = ComponentIndex<T>.FindIn(_contextInfo);
			if (ignoreNotFound && !HasComponent(index))
				return;

			RemoveComponent(index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void RemoveComponent<T>(bool ignoreNotFound = true) where T : IComponent
		{
			Remove<T>(ignoreNotFound);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Has<T>() where T : IComponent
		{
			int index = ComponentIndex<T>.FindIn(_contextInfo);
			return HasComponent(index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool HasComponent<T>() where T : IComponent
		{
			return Has<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T Get<T>() where T : IComponent
		{
			int index = ComponentIndex<T>.FindIn(_contextInfo);
			return (T)GetComponent(index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T GetComponent<T>() where T : IComponent
		{
			return Get<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		/// Get Component for modification, mark automatically
		public T Modify<T>() where T : IComponent
		{
			int index = ComponentIndex<T>.FindIn(_contextInfo);
			return (T)ModifyComponent(index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T ModifyComponent<T>() where T : IComponent
		{
			return Modify<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IComponent ModifyComponent(int index)
		{
			var component = GetComponent(index);
			FireModifiedEvent(index, component);
			return component;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		/// Mark component modified, trigger GroupEvent and ReactiveSystem
		public void SetModified<T>() where T : IComponent, new()
		{
			int index = ComponentIndex<T>.FindIn(_contextInfo);
			SetModified(index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetModified(int index)
		{
			FireModifiedEvent(index, GetComponent(index));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void FireModifiedEvent(int index, IComponent component)
		{
			// Set modified flag
			var modifiable = component as IModifiable;
			if (modifiable != null)
				modifiable.modified = true;

			if (OnComponentReplaced != null)
				OnComponentReplaced(this, index, component, component);
		}
	}
}
