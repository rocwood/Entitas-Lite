
namespace Entitas
{
	/// using ComponentIndex<T> to Component->Index mapping
	public partial class Entity
	{
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

		public T AddComponent<T>(bool useExisted = true) where T : IComponent, new()
		{
			return Add<T>(useExisted);
		}

		/// replace Component with a NEW one
		public T ReplaceNew<T>() where T : IComponent, new()
		{
			int index = ComponentIndex<T>.FindIn(_contextInfo);

			T component = CreateComponent<T>(index);
			ReplaceComponent(index, component);

			return component;
		}

		public T ReplaceNewComponent<T>() where T : IComponent, new()
		{
			return ReplaceNew<T>();
		}

		public void Remove<T>(bool ignoreNotFound = true) where T: IComponent
		{
			int index = ComponentIndex<T>.FindIn(_contextInfo);
			if (ignoreNotFound && !HasComponent(index))
				return;

			RemoveComponent(index);
		}

		public void RemoveComponent<T>(bool ignoreNotFound = true) where T : IComponent
		{
			Remove<T>(ignoreNotFound);
		}
		
		public bool Has<T>() where T : IComponent
		{
			int index = ComponentIndex<T>.FindIn(_contextInfo);
			return HasComponent(index);
		}

		public bool HasComponent<T>() where T : IComponent
		{
			return Has<T>();
		}

		public T Get<T>() where T : IComponent
		{
			int index = ComponentIndex<T>.FindIn(_contextInfo);
			return (T)GetComponent(index);
		}

		public T GetComponent<T>() where T : IComponent
		{
			return Get<T>();
		}

		/// Get Component for modification, mark automatically
		public T Modify<T>() where T : IComponent
		{
			int index = ComponentIndex<T>.FindIn(_contextInfo);
			return (T)ModifyComponent(index);
		}

		public T ModifyComponent<T>() where T : IComponent
		{
			return Modify<T>();
		}

		public IComponent ModifyComponent(int index)
		{
			var component = GetComponent(index);
			FireModifiedEvent(index, component);
			return component;
		}

		/// Mark component modified, trigger GroupEvent and ReactiveSystem
		public void SetModified<T>() where T : IComponent, new()
		{
			int index = ComponentIndex<T>.FindIn(_contextInfo);
			SetModified(index);
		}

		public void SetModified(int index)
		{
			FireModifiedEvent(index, GetComponent(index));
		}

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
