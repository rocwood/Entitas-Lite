using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Entitas
{
	class EntityManager
	{
		private const int DefaultCapacity = 256;
		private const int MaxCapacity = 0x7FEFFFFF;

		private readonly EntityPool _pool;
		private readonly Dictionary<int, Entity> _lookup;
		private Entity[] _items;

		private readonly Dictionary<int, Entity> _modifiedSet;

		private volatile int _count = 0;
		private volatile int _lastId = 0;

		public int Count
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _count;
		}

		public Entity this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _items[index];
		}

		internal EntityManager(int capacity, int maxRetained)
		{
			if (capacity <= 0)
				capacity = DefaultCapacity;
			else if ((uint)capacity > MaxCapacity)
				capacity = MaxCapacity;

			_pool = new EntityPool(maxRetained);

			_items = new Entity[capacity];
			_lookup = new Dictionary<int, Entity>(capacity);

			_modifiedSet = new Dictionary<int, Entity>(capacity);
		}

		public Entity CreateEntity(string name, IComponentPool[] componentPools)
		{
			int id = Interlocked.Increment(ref _lastId);

			var entity = _pool.Get();
			entity.Init(componentPools, _modifiedSet);
			entity.Active(id, name);

			EnsureAccess(_count);

			_items[_count++] = entity;
			_lookup.Add(id, entity);

			return entity;
		}

		public Entity GetEntity(int id)
		{
			_lookup.TryGetValue(id, out var entity);
			return entity;
		}

		public void GetEntities(IList<Entity> output)
		{
			for (int i = 0; i < _count; i++)
				output.Add(_items[i]);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IReadOnlyCollection<Entity> GetModifiedEntities()
		{
			return _modifiedSet.Values;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ResetModifiedEntities()
		{
			_modifiedSet.Clear();
		}

		private void EnsureAccess(int index)
		{
			int size = _items.Length;
			if (index < size)
				return;

			while (index >= size)
			{
				if (size <= 0)
					size = DefaultCapacity;
				else
					size *= 2;

				if ((uint)size > MaxCapacity)
				{
					size = MaxCapacity;
					break;
				}
			}

			if (size > _items.Length)
				Array.Resize(ref _items, size);
		}

		public void DestroyDisabledEntities()
		{
			for (int i = 0; i < _count; i++)
			{
				var e = _items[i];
				if (e.isEnabled)
					continue;

				_lookup.Remove(e.id);

				e.InternalDestroy();
				_pool.Return(e);

				_items[i] = null;
			}

			RemoveNullEntities();
		}

		private void RemoveNullEntities()
		{
			int freeIndex = 0;

			// Find the first null item
			while (freeIndex < _count && _items[freeIndex] != null) freeIndex++;
			if (freeIndex >= _count) return;

			int current = freeIndex + 1;
			while (current < _count)
			{
				// Find the first item which needs to be kept.
				while (current < _count && _items[current] == null) current++;

				if (current < _count)
					_items[freeIndex++] = _items[current++];
			}

			Array.Clear(_items, freeIndex, _count - freeIndex);

			_count = freeIndex;
		}
	}
}
