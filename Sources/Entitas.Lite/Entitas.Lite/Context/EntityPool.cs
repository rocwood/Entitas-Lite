#if THREADSAFE_POOL
using Microsoft.Extensions.ObjectPool;
#else
using System.Runtime.CompilerServices;
using Entitas.Utils;
#endif

namespace Entitas
{
	class EntityPool
	{
#if THREADSAFE_POOL
		private readonly ObjectPool<Entity> _pool;

		public EntityPool(int maxRetained = 0)
		{
			var provider = new DefaultObjectPoolProvider();
			if (maxRetained > 0)
				provider.MaximumRetained = maxRetained;

			var policy = new PoolPolicy();

			_pool = provider.Create(policy);
		}
#else
		private readonly ObjectPool<Entity> _pool;

		public EntityPool(int maxRetained = 0)
		{
			_pool = new ObjectPool<Entity>(new PoolPolicy(), maxRetained);
		}
#endif
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Entity Get()
		{
			return _pool.Get();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Return(Entity obj)
		{
			_pool.Return(obj);
		}

#if THREADSAFE_POOL
		class PoolPolicy : IPooledObjectPolicy<Entity>
#else
		class PoolPolicy : ObjectPool<Entity>.Policy
#endif
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public Entity Create()
			{
				return new Entity();
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool Return(Entity obj)
			{
				if (obj == null)
					return false;

				return true;
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Dispose(Entity obj)
			{
			}
		}
	}
}
