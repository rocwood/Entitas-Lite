using System.Collections.Generic;

namespace Entitas
{
	public partial class Context
	{
		public static int defaultEntityCapacity = 1024;
		public static int maxRetainedEntities = 256;
		public static int maxRetainedComponents = 128;

		private readonly string _contextName;
		private readonly IComponentPool[] _componentPools;

		private readonly EntityManager _entities = new EntityManager(defaultEntityCapacity, maxRetainedEntities);
		private readonly Dictionary<Matcher, Group> _groups = new Dictionary<Matcher, Group>();

		public int Count => _entities.Count;

		internal Context(string contextName)
		{
			_contextName = contextName;

			int componentCount = _contextInfo.GetComponentCount();
			_componentPools = new IComponentPool[componentCount];

			for (int i = 0; i < componentCount; i++)
				_componentPools[i] = ComponentPoolFactory.Create(_contextInfo.componentTypes[i], maxRetainedComponents);
		}

		public Entity CreateEntity(string entityName = null)
		{
			return _entities.CreateEntity(entityName, _componentPools);
		}

		public Entity GetEntity(int id)
		{
			return _entities.GetEntity(id);
		}

		public void GetEntities(IList<Entity> output)
		{
			_entities.GetEntities(output);
		}

		public void Poll()
		{
			HandleGroupChanges();

			_entities.DestroyDisabledEntities();
		}

		/// Returns a group for the specified matcher.
		/// Calling context.GetGroup(matcher) with the same matcher will always
		/// return the same instance of the group.
		public Group GetGroup(Matcher matcher)
		{
			if (!_groups.TryGetValue(matcher, out var group))
			{
				group = new Group(matcher);

				for (int i = 0; i < _entities.Count; i++)
				{
					var entity = _entities[i];

					if (entity.isEnabled)
						group.HandleEntity(entity);
				}

				_groups.Add(matcher, group);
			}

			return group;
		}

		private void HandleGroupChanges()
		{
			int count = _entities.Count;

			for (int i = 0; i < count; i++)
			{
				var entity = _entities[i];

				if (!entity.isEnabled || entity.isModified)
				{
					foreach (var kv in _groups)
						kv.Value?.HandleEntity(entity);

					entity.ResetModified();
				}
			}
		}

		private string _toStringCache;

		public override string ToString()
		{
			if (_toStringCache == null)
				_toStringCache = $"Context<{_contextName}>";

			return _toStringCache;
		}
	}
}
