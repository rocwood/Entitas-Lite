using Microsoft.Extensions.ObjectPool;

namespace Entitas
{
	/// <summary>
	/// Component's Pool
	/// </summary>
	public class ComponentPool<T> : ObjectPool<T> where T:class, IComponent, new()
	{
		private readonly ObjectPool<T> _pool;

		public ComponentPool(int maxRetained = 0, IPooledObjectPolicy<T> policy = null)
		{
			_pool = ComponentPoolProvider.Create<T>(maxRetained, policy);
		}

		public override T Get()
		{
			return _pool.Get();
		}

		public override void Return(T obj)
		{
			if (obj != null)
				_pool.Return(obj);
		}
	}

	static class ComponentPoolProvider
	{
		/// Use unique instance for empty components
		class OneInstancePool<T> : ObjectPool<T> where T : class, IComponent, new()
		{
			private T _instance = new T();

			public override T Get() => _instance;
			public override void Return(T obj) {}
		}

		public static ObjectPool<T> Create<T>(int maxRetained = 0, IPooledObjectPolicy<T> policy = null) where T:class, IComponent, new()
		{
			if (CheckEmptyComponent.IsEmpty<T>())
				return new OneInstancePool<T>();

			var provider = new DefaultObjectPoolProvider();

			if (maxRetained > 0)
				provider.MaximumRetained = maxRetained;

			return provider.Create(policy ?? new DefaultPooledObjectPolicy<T>());
		}
	}
}
