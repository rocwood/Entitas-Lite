using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Entitas
{
	public partial class Context
	{
		public static int defaultEntityCapacity = 1024;
		public static int defaultModifiedCapacity = 256;
		public static int maxRetainedEntities = 128;

		private readonly SimplePool<Entity> _entityPool = new SimplePool<Entity>(maxRetainedEntities);

		private readonly EntityTable _entities = new EntityTable(defaultEntityCapacity);
		private readonly Dictionary<int, Entity> _entitiesModified = new Dictionary<int, Entity>(defaultModifiedCapacity);

		private int _lastId = 0;

		public int Count
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _entities.Count;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Entity CreateEntity(string entityName = null)
		{
			var id = ++_lastId;

			var entity = _entityPool.Get() ?? new Entity(this, _componentPools);
			entity.Activate(id, entityName);

			_entities.Add(entity);

			return entity;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void MarkModified(Entity e)
		{
			_entitiesModified[e.id] = e;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Entity GetEntity(int id)
		{
			return _entities[id];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetEntities(IList<Entity> output)
		{
			_entities.CopyTo(output);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IEnumerator<Entity> GetEnumerator()
		{
			return _entities.GetEnumerator();
		}
	}
}
