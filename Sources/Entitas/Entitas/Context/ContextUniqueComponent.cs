namespace Entitas
{
	public partial class Context
	{
		/// Returns unique entity with specified component
		public Entity GetSingleEntity<T>() where T : IComponent, IUnique
		{
			return GetSingleEntity(ComponentIndex<T>.Get());
		}

		public Entity GetSingleEntity(int componentIndex)
		{
			IGroup group = _groupForSingle[componentIndex];

			if (group == null)
			{
				group = GetGroup(Matcher.AllOf(componentIndex));
				_groupForSingle[componentIndex] = group;
			}

			return group.GetSingleEntity();
		}

		public T GetUnique<T>() where T : IComponent, IUnique
		{
			int componentIndex = ComponentIndex<T>.Get();

			IComponent component = GetUniqueComponent(componentIndex);
			if (component == null)
				return default(T);

			return (T)component;
		}

		private IComponent GetUniqueComponent(int componentIndex)
		{
			Entity entity = GetSingleEntity(componentIndex);
			if (entity == null)
				return null;

			return entity.GetComponent(componentIndex);
		}

		public T AddUnique<T>(bool useExisted = true) where T : IComponent, IUnique, new()
		{
			int componentIndex = ComponentIndex<T>.Get();

			Entity entity = GetSingleEntity(componentIndex);
			if (entity != null)
			{
				if (!useExisted)
					throw new EntityAlreadyHasComponentException(
					   componentIndex, "Cannot add component '" +
					   _contextInfo.componentNames[componentIndex] + "' to " + entity + "!",
					   "You should check if an entity already has the component."
					);
				return (T)ModifyUniqueComponent(componentIndex);
			}

			entity = CreateEntity(typeof(T).Name);
			return (T)entity.AddComponent(componentIndex);
		}

		public T ModifyUnique<T>() where T : IComponent, IUnique
		{
			int componentIndex = ComponentIndex<T>.Get();

			IComponent component = ModifyUniqueComponent(componentIndex);
			if (component == null)
				return default(T);

			return (T)component;
		}

		private IComponent ModifyUniqueComponent(int componentIndex)
		{
			Entity entity = GetSingleEntity(componentIndex);
			if (entity == null)
				return null;

			var component = entity.GetComponent(componentIndex);
			component?.Modify();

			return component;
		}
	}
}
