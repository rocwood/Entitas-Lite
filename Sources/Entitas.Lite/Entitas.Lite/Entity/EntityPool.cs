using Microsoft.Extensions.ObjectPool;

namespace Entitas
{
	class EntityPool
	{
		private readonly ObjectPool<Entity> _pool;

		public EntityPool(int maxRetained = 0)
		{
			var provider = new DefaultObjectPoolProvider();
			if (maxRetained > 0)
				provider.MaximumRetained = maxRetained;

			var policy = new PoolPolicy();

			_pool = provider.Create(policy);
		}

		public Entity Get()
		{
			return _pool.Get();
		}

		public void Return(Entity obj)
		{
			if (obj != null)
				_pool.Return(obj);
		}

		class PoolPolicy : IPooledObjectPolicy<Entity>
		{
			public Entity Create()
			{
				return new Entity();
			}

			public bool Return(Entity obj)
			{
				if (obj == null)
					return false;

				// TODO

				return true;
			}
		}
	}
}
