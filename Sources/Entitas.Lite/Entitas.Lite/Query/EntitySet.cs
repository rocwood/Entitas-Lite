using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Entitas
{
	class EntitySet
	{
		private const int DefaultCapacity = 16;

		private readonly Dictionary<int, int> _lookup;
		private Entity[] _items;
		private int _count;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal EntitySet(int capacity = 0)
		{
			if (capacity <= 0)
				capacity = DefaultCapacity;

			_lookup = new Dictionary<int, int>(capacity);
			_items = new Entity[capacity];
		}

		public int Count
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _count;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Entity GetAt(int index)
		{
			return _items[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add(Entity e)
		{
			int id = e.id;
			if (id <= 0)
				return;

			if (_lookup.TryGetValue(id, out var index))
			{
				_items[index] = e;
			}
			else
			{
				if (_count >= _items.Length)
					EnsureLength(_count + 1);

				_items[_count] = e;
				_lookup[id] = _count;

				_count++;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Remove(Entity e)
		{
			if (_count <= 0)
				return;

			int id = e.id;
			if (id <= 0)
				return;

			if (!_lookup.TryGetValue(id, out var index))
				return;

			_lookup.Remove(id);

			int last = _count - 1;
			if (index < last)
			{
				ref var el = ref _items[last];
				
				_items[index] = el;
				_lookup[el.id] = index;

				el = default;
			}

			_count--;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clear()
		{
			_lookup.Clear();
			_count = 0;

			Array.Clear(_items, 0, _items.Length);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void EnsureLength(int minSize)
		{
			int size = _items.Length;
			if (size >= minSize)
				return;

			if (size <= 0)
				size = DefaultCapacity;

			while (size < minSize)
				size *= 2;

			if (size > _items.Length)
				Array.Resize(ref _items, size);
		}

		private List _cachedList;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IReadOnlyList<Entity> GetEntities()
		{
			if (_cachedList == null)
				_cachedList = new List(this);

			return _cachedList;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IEnumerator<Entity> GetEnumerator()
		{
			return new Enumerator(this);
		}

		public class List : IReadOnlyList<Entity>
		{
			private readonly EntitySet _container;

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal List(EntitySet container)
			{
				_container = container;
			}

			public Entity this[int index]
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get => _container.GetAt(index);
			}

			public int Count
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get => _container.Count;
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public IEnumerator<Entity> GetEnumerator()
			{
				return new Enumerator(_container);
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}
		}

		public struct Enumerator : IEnumerator<Entity>
		{
			private readonly EntitySet _container;
			private readonly int _count;
			private int _index;

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal Enumerator(EntitySet container)
			{
				_container = container;
				_count = container.Count;
				_index = -1;
			}

			public Entity Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get => _container._items[_index];
			}

			object IEnumerator.Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get => this.Current;
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				return ++_index < _count;
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Reset()
			{
				_index = -1;
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Dispose()
			{
			}
		}
	}
}
