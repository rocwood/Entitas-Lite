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

		internal Matcher matcher => _matcher;

		private readonly Matcher _matcher;
		private readonly SortedList<int, Entity> _entities = new SortedList<int, Entity>();

		private Entity[] _entitiesCache;
		private Entity _singleEntityCache;
		private string _toStringCache;

		/// Use context.GetGroup(matcher) to get a group of entities which match the specified matcher.
		internal Group(Matcher matcher)
		{
			_matcher = matcher;
		}

		/// This is used by the context to manage the group.
		internal void HandleEntity(Entity entity)
		{
			if (entity.isEnabled && _matcher.Matches(entity))
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
			_singleEntityCache = null;

			return true;
		}

		private bool RemoveEntityImpl(Entity entity)
		{
			if (!_entities.Remove(entity.id))
				return false;

			_entitiesCache = null;
			_singleEntityCache = null;

			return true;
		}

		public bool Contains(Entity entity)
		{
			if (!_entities.TryGetValue(entity.id, out var item))
				return false;

			return entity == item;
		}

		/// Returns all entities which are currently in this group.
		public Entity[] GetEntities()
		{
			if (_entitiesCache == null)
			{
				_entitiesCache = new Entity[_entities.Count];
				_entities.Values.CopyTo(_entitiesCache, 0);
			}

			return _entitiesCache;
		}

		/*
		/// Returns the only entity in this group. It will return null
		/// if the group is empty. It will throw an exception if the group
		/// has more than one entity.
		public Entity GetSingleEntity()
		{
			if (_singleEntityCache == null)
			{
				var c = _entities.Count;
				if (c == 1)
				{
					using (var enumerator = _entities.GetEnumerator())
					{
						enumerator.MoveNext();
						_singleEntityCache = enumerator.Current;
					}
				}
				else if (c == 0)
				{
					return null;
				}
				else
				{
					throw new GroupSingleEntityException(this);
				}
			}

			return _singleEntityCache;
		}
		*/

		public override string ToString()
		{
			if (_toStringCache == null)
				_toStringCache = $"Group({_matcher})";

			return _toStringCache;
		}
	}
}
