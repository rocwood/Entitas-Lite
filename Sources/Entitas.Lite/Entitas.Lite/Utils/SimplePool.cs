using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Entitas
{
	class SimplePool<T> where T:class
	{
		private readonly int _maxRetained;
		private readonly List<T> _pool;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public SimplePool(int maxRetained = 0)
		{
			_maxRetained = maxRetained;
			_pool = maxRetained > 0
				? new List<T>(maxRetained)
				: new List<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T Get()
		{
			int last = _pool.Count - 1;
			if (last < 0)
				return null;

			T result = _pool[last];
			_pool.RemoveAt(last);

			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Return(T obj)
		{
			if (obj == null)
				return false;

			if (_maxRetained > 0 && _pool.Count >= _maxRetained)
				return false;

			_pool.Add(obj);
			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clear()
		{
			_pool.Clear();
		}
	}
}
