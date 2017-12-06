
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
		
		/// replace Component with a NEW one
		public T ReplaceNew<T>() where T : IComponent, new()
		{
			int index = ComponentIndex<T>.FindIn(_contextInfo);

			T component = CreateComponent<T>(index);
			ReplaceComponent(index, component);

			return component;
		}

		public void Remove<T>(bool ignoreNotFound = true) where T: IComponent
		{
			int index = ComponentIndex<T>.FindIn(_contextInfo);
			if (ignoreNotFound && !HasComponent(index))
				return;

			RemoveComponent(index);
		}

		public bool Has<T>() where T : IComponent
		{
			int index = ComponentIndex<T>.FindIn(_contextInfo);
			return HasComponent(index);
		}

		/// shorter version of GetComponent<T>
		public T Get<T>() where T : IComponent
		{
			int index = ComponentIndex<T>.FindIn(_contextInfo);
			return (T)GetComponent(index);
		}

		/// Get Component for modification, mark automatically
		public T Modify<T>() where T : IComponent
		{
			int index = ComponentIndex<T>.FindIn(_contextInfo);
			return (T)Modify(index);
		}

		public IComponent Modify(int index)
		{
			IComponent component = GetComponent(index);
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
			if (OnComponentReplaced != null)
				OnComponentReplaced(this, index, component, component);
		}
	}
}
