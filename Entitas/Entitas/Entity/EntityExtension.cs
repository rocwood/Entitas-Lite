
namespace Entitas
{
	/// Extension for Entity, using ComponentIndex<T> to Component->Index mapping
	public static class EntityExtension
	{
		/// add a new Component, return the old one if exists
		public static T Add<T>(this Entity entity, bool useExisted = true) where T : IComponent, new()
		{
			T component;

			int index = ComponentIndex<T>.FindIn(entity.contextInfo);

			if (useExisted && entity.HasComponent(index))
			{
				component = (T)entity.GetComponent(index);
				entity.SetModified(index);
			}
			else
			{
				component = entity.CreateComponent<T>(index);
				entity.AddComponent(index, component);
			}

			return component;
		}
		
		/// replace Component with a NEW one
		public static T ReplaceNew<T>(this Entity entity) where T : IComponent, new()
		{
			int index = ComponentIndex<T>.FindIn(entity.contextInfo);

			T component = entity.CreateComponent<T>(index);
			entity.ReplaceComponent(index, component);

			return component;
		}

		public static void Remove<T>(this Entity entity, bool ignoreNotFound = true) where T: IComponent
		{
			int index = ComponentIndex<T>.FindIn(entity.contextInfo);
			if (ignoreNotFound && !entity.HasComponent(index))
				return;

			entity.RemoveComponent(index);
		}

		public static bool Has<T>(this Entity entity) where T : IComponent
		{
			int index = ComponentIndex<T>.FindIn(entity.contextInfo);
			return entity.HasComponent(index);
		}

		/// shorter version of GetComponent<T>
		public static T Get<T>(this Entity entity) where T : IComponent
		{
			int index = ComponentIndex<T>.FindIn(entity.contextInfo);
			return (T)entity.GetComponent(index);
		}

		/// Get Component for modification, mark automatically
		public static T Modify<T>(this Entity entity) where T : IComponent
		{
			int index = ComponentIndex<T>.FindIn(entity.contextInfo);
			T component = (T)entity.GetComponent(index);

			entity.SetModified(index);
			return component;
		}

		/// Mark component modified, trigger GroupEvent and ReactiveSystem
		public static void SetModified<T>(this Entity entity) where T : IComponent, new()
		{
			int index = ComponentIndex<T>.FindIn(entity.contextInfo);
			entity.SetModified(index);
		}

		public static void SetModified(this Entity entity, int index)
		{
			entity.ReplaceComponent(index, entity.GetComponent(index));
		}
	}
}
