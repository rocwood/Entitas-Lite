using System;
using Microsoft.Extensions.ObjectPool;

namespace Entitas
{
	public interface IComponentPool
	{
		IComponent Get();
		void Return(IComponent obj);
	}

	/// <summary>
	/// Common component's pool
	/// </summary>
	class ComponentPool : IComponentPool
	{
		private readonly ObjectPool<IComponent> _pool;
	
		public ComponentPool(Type objType, int maxRetained = 0)
		{
			var provider = new DefaultObjectPoolProvider();
			if (maxRetained > 0)
				provider.MaximumRetained = maxRetained;

			var policy = new PoolPolicy(objType);

			_pool = provider.Create(policy);
		}

		public IComponent Get()
		{
			return _pool.Get();
		}

		public void Return(IComponent obj)
		{
			if (obj != null)
				_pool.Return(obj);
		}

		class PoolPolicy : IPooledObjectPolicy<IComponent>
		{
			private readonly Type _objType;

			public PoolPolicy(Type objType)
			{
				_objType = objType;
			}

			public IComponent Create()
			{
				return (IComponent)Activator.CreateInstance(_objType);
			}

			public bool Return(IComponent obj)
			{
				if (obj == null)
					return false;

				// Reset component status before returning to pool
				if (obj is IEntityIdRef entityIdRef)
					entityIdRef.entityId = 0;
				if (obj is IModifiable modifiable)
					modifiable.Accept();
				if (obj is IResetable resetable)
					resetable.Reset();
				if (obj is IDisposable disposable)
					disposable.Dispose();
				
				return true;
			}
		}
	}

	/// <summary>
	/// Unique instance pool for zero-size components
	/// </summary>
	class ZeroSizeComponentPool : IComponentPool
	{
		private readonly IComponent _instance;

		public ZeroSizeComponentPool(Type objType)
		{
			_instance = (IComponent)Activator.CreateInstance(objType);
		}

		public IComponent Get() => _instance;
		public void Return(IComponent obj) { }
	}

	internal static class ComponentPoolFactory
	{
		public static IComponentPool Create(Type objType, int maxRetained = 0)
		{
			if (!objType.IsAssignableFrom(typeof(IComponent)))
				throw new ArgumentException($"{objType.FullName} isn't IComponent");

			if (ComponentChecker.IsZeroSize(objType))
				return new ZeroSizeComponentPool(objType);
			else
				return new ComponentPool(objType, maxRetained);
		}
	}
}
