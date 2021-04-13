using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Entitas
{
	public partial class Context
	{
		public static int defaultEntityCapacity = 1024;
		public static int defaultModifiedCapacity = 256;
		public static int maxRetainedEntities = 128;

		private readonly EntityTable _entities = new EntityTable(defaultEntityCapacity);
		private readonly EntityPool _entityPool = new EntityPool(maxRetainedEntities);
		private readonly Dictionary<int, Entity> _modifiedEntities = new Dictionary<int, Entity>(defaultModifiedCapacity);

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

			var entity = _entityPool.Get();
			entity.Init(_componentPools, _modifiedEntities);
			entity.Active(id, entityName);

			_entities.Add(entity);

			return entity;
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void DestroyDisabledEntities()
		{
			var modifiedEntities = _modifiedEntities.Values;
			if (modifiedEntities.Count <= 0)
				return;

			foreach (var e in modifiedEntities)
			{
				if (e.isEnabled)
					continue;

				_entities.Remove(e.id);

				e.InternalDestroy();
				_entityPool.Return(e);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ResetModifiedEntities()
		{
			_modifiedEntities.Clear();
		}
	}
}
