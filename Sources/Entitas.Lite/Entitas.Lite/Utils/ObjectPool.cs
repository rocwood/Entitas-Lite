using System.Collections.Generic;

namespace Entitas.Utils
{
	class ObjectPool<T> where T:class
	{
		public interface Policy
		{
			T Create();
			bool Return(T obj);
			void Dispose(T obj);
		}

		private readonly int _maxRetained;
		private readonly Policy _policy;
		private readonly List<T> _pool;

		public ObjectPool(Policy policy, int maxRetained = 0)
		{
			_maxRetained = maxRetained;
			_policy = policy;
			_pool = maxRetained > 0
				? new List<T>(maxRetained)
				: new List<T>();
		}

		public T Get()
		{
			T result;

			int last = _pool.Count - 1;
			if (last >= 0)
			{
				result = _pool[last];
				_pool.RemoveAt(last);
			}
			else
			{
				result = _policy.Create();
			}

			return result;
		}

		public void Return(T obj)
		{
			if (obj == null || _pool.IndexOf(obj) >= 0)
				return;

			if (!_policy.Return(obj))
				return;

			if (_maxRetained > 0 && _pool.Count >= _maxRetained)
			{
				_policy.Dispose(obj);
				return;
			}

			_pool.Add(obj);
		}

		public void Clear()
		{
			for (int i = 0; i < _pool.Count; i++)
				_policy.Dispose(_pool[i]);

			_pool.Clear();
		}
	}
}
