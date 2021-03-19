using System.Collections.Generic;

namespace Entitas
{
	public class Group
	{
		private readonly Matcher _matcher;
		private readonly SortedList<int, Entity> _entities = new SortedList<int, Entity>();

		private readonly List<Entity> _entitiesCache = new List<Entity>();
		private bool _hasCached = false;

		public int Count => _entities.Count;

		internal Group(Matcher matcher)
		{
			_matcher = matcher;
		}

		internal void HandleEntity(Entity entity)
		{
			if (entity == null)
				return;

			if (entity.isEnabled && _matcher.Matches(entity))
				HandleAddEntity(entity);
			else
				HandleRemoveEntity(entity);
		}

		private void HandleAddEntity(Entity entity)
		{
			_entities.TryGetValue(entity.id, out var item);
			if (entity == item)
				return;

			_entities[entity.id] = entity;

			_hasCached = false;
		}

		private void HandleRemoveEntity(Entity entity)
		{
			if (!_entities.Remove(entity.id))
				return;
				
			_hasCached = false;
		}

		public bool Contains(Entity entity)
		{
			if (entity == null)
				return false;

			if (!_entities.TryGetValue(entity.id, out var item))
				return false;

			return entity == item;
		}

		public IReadOnlyList<Entity> GetEntities()
		{
			if (!_hasCached)
			{
				_entitiesCache.Clear();

				var values = _entities.Values;

				if (_entitiesCache.Capacity < values.Count)
					_entitiesCache.Capacity = values.Count;

				for (int i = 0; i < values.Count; i++)
					_entitiesCache.Add(values[i]);
			}

			return _entitiesCache;
		}

		public void GetEntities(IList<Entity> output)
		{
			output.Clear();

			var values = _entities.Values;

			for (int i = 0; i < values.Count; i++)
				output.Add(values[i]);
		}

		public override string ToString()
		{
			if (_toStringCache == null)
				_toStringCache = $"Group({_matcher})";

			return _toStringCache;
		}

		private string _toStringCache;
	}
}
