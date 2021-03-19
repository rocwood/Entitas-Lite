using System.Collections.Generic;

namespace Entitas
{

	public class Context
	{
		public static int defaultEntityCapacity = 1024;
		public static int maxRetainedEntities = 256;
		public static int maxRetainedComponents = 128;

		private readonly string _name;
		private readonly ContextInfo _contextInfo;
		private readonly IComponentPool[] _componentPools;

		private readonly EntityManager _entities = new EntityManager(defaultEntityCapacity, maxRetainedEntities);
		private readonly Dictionary<Query, Group> _groups = new Dictionary<Query, Group>();

		public int Count => _entities.Count;

		internal Context(string name, ContextInfo contextInfo)
		{
			_name = name;
			_contextInfo = contextInfo;

			int totalComponents = _contextInfo.GetComponentCount();

			_componentPools = new IComponentPool[totalComponents];

			for (int i = 0; i < totalComponents; i++)
				_componentPools[i] = ComponentPoolFactory.Create(contextInfo.componentTypes[i], maxRetainedComponents);
		}

		public Entity CreateEntity(string name = null)
		{
			var entity = _entities.CreateEntity(name);
			entity.SetProvider(_contextInfo, _componentPools);

			return entity;
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
		public Group GetGroup(Query matcher)
		{
			if (!_groups.TryGetValue(matcher, out var group))
			{
				group = new Group(matcher);

				for (int i = 0; i < _entities.Count; i++)
					group.HandleEntity(_entities[i]);

				_groups.Add(matcher, group);

				/*
				for (int i = 0; i < matcher.indices.Length; i++)
				{
					var index = matcher.indices[i];

					if (_groupsForIndex[index] == null)
						_groupsForIndex[index] = new List<IGroup>();

					_groupsForIndex[index].Add(group);
				}
				*/

				//OnGroupCreated?.Invoke(this, group);
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
				}
			}
		}

		private string _toStringCache;

		public override string ToString()
		{
			if (_toStringCache == null)
				_toStringCache = $"Context<{_name}>";

			return _toStringCache;
		}
	}
}
