using System.Collections;
using System.Collections.Generic;

namespace Entitas
{
	/// Use context.GetGroup(matcher) to get a group of entities which match
	/// the specified matcher. Calling context.GetGroup(matcher) with the
	/// same matcher will always return the same instance of the group.
	/// The created group is managed by the context and will always be up to date.
	/// It will automatically add entities that match the matcher or
	/// remove entities as soon as they don't match the matcher anymore.
	public class Group
	{
		public int Count => _entities.Count;

		private readonly Query _query;
		private readonly SortedList<int, Entity> _entities = new SortedList<int, Entity>();

		private Entity[] _entitiesCache;

		internal Group(Query matcher)
		{
			_query = matcher;
		}

		internal void HandleEntity(Entity entity)
		{
			if (entity.isEnabled && _query.Matches(entity))
				AddEntityImpl(entity);
			else
				RemoveEntityImpl(entity);
		}

		private bool AddEntityImpl(Entity entity)
		{
			if (Contains(entity))
				return false;

			_entities[entity.id] = entity;

			_entitiesCache = null;

			return true;
		}

		private bool RemoveEntityImpl(Entity entity)
		{
			if (!_entities.Remove(entity.id))
				return false;

			_entitiesCache = null;

			return true;
		}

		public bool Contains(Entity entity)
		{
			if (!_entities.TryGetValue(entity.id, out var item))
				return false;

			return entity == item;
		}

		public Entity[] GetEntities()
		{
			if (_entitiesCache == null)
			{
				_entitiesCache = new Entity[_entities.Count];
				_entities.Values.CopyTo(_entitiesCache, 0);
			}

			return _entitiesCache;
		}

		public override string ToString()
		{
			if (_toStringCache == null)
				_toStringCache = $"Group({_query})";

			return _toStringCache;
		}

		private string _toStringCache;
	}
}
