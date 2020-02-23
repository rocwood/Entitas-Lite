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
		/*
		/// Occurs when an entity gets added.
		public event GroupChanged OnEntityAdded;

		/// Occurs when an entity gets removed.
		public event GroupChanged OnEntityRemoved;

		/// Occurs when a component of an entity in the group gets replaced.
		public event GroupUpdated OnEntityUpdated;
		*/

		/// Returns the number of entities in the group.
		public int count => _entities.Count;

		/// Returns the matcher which was used to create this group.
		public Matcher matcher => _matcher;

		readonly Matcher _matcher;
		readonly SortedList<int, Entity> _entities = new SortedList<int, Entity>();

		Entity[] _entitiesCache;
		Entity _singleEntityCache;
		string _toStringCache;

		/// Use context.GetGroup(matcher) to get a group of entities which match
		/// the specified matcher.
		internal Group(Matcher matcher)
		{
			_matcher = matcher;
		}

		/// This is used by the context to manage the group.
		internal void HandleEntity(Entity entity)
		{
			if (_matcher.Matches(entity))
				addEntitySilently(entity);
			else
				removeEntitySilently(entity);
		}

		/*
		/// This is used by the context to manage the group.
		public void UpdateEntity(Entity entity, int index, IComponent previousComponent, IComponent newComponent)
		{
			if (_entities.Contains(entity))
			{
				if (OnEntityRemoved != null)
				{
					OnEntityRemoved(this, entity, index, previousComponent);
				}
				if (OnEntityAdded != null)
				{
					OnEntityAdded(this, entity, index, newComponent);
				}
				if (OnEntityUpdated != null)
				{
					OnEntityUpdated(this, entity, index, previousComponent, newComponent);
				}
			}
		}

		/// This is called by context.Reset() to remove all event handlers.
		/// This is useful when you want to soft-restart your application.
		public void RemoveAllEventHandlers()
		{
			OnEntityAdded = null;
			OnEntityRemoved = null;
			OnEntityUpdated = null;
		}

		public GroupChanged HandleEntity(Entity entity)
		{
			return _matcher.Matches(entity)
					   ? (addEntitySilently(entity) ? OnEntityAdded : null)
					   : (removeEntitySilently(entity) ? OnEntityRemoved : null);
		}
		*/

		bool addEntitySilently(Entity entity)
		{
			if (!entity.isEnabled)
				return false;

			if (Contains(entity))
				return false;

			_entities[entity.id] = entity;

			_entitiesCache = null;
			_singleEntityCache = null;

			return true;
		}

		bool removeEntitySilently(Entity entity)
		{
			if (!_entities.Remove(entity.id))
				return false;

			_entitiesCache = null;
			_singleEntityCache = null;

			return true;
		}

		/// Determines whether this group has the specified entity.
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
