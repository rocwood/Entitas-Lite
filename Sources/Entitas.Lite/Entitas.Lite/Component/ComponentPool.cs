using System;
using System.Linq;
using System.Reflection;

#if THREADSAFE_POOL
using Microsoft.Extensions.ObjectPool;
#else
using Entitas.Utils;
#endif

namespace Entitas
{
	public interface IComponentPool
	{
		IComponent Get();
		void Return(IComponent obj);
	}

	class ComponentPool : IComponentPool
	{
#if THREADSAFE_POOL
		private readonly ObjectPool<IComponent> _pool;
	
		public ComponentPool(Type objType, int maxRetained = 0)
		{
			var provider = new DefaultObjectPoolProvider();
			if (maxRetained > 0)
				provider.MaximumRetained = maxRetained;

			var policy = new PoolPolicy(objType);

			_pool = provider.Create(policy);
		}
#else
		private readonly ObjectPool<IComponent> _pool;

		public ComponentPool(Type objType, int maxRetained = 0)
		{
			_pool = new ObjectPool<IComponent>(new PoolPolicy(objType), maxRetained);
		}
#endif

		public IComponent Get()
		{
			return _pool.Get();
		}

		public void Return(IComponent obj)
		{
			_pool.Return(obj);
		}

#if THREADSAFE_POOL
		class PoolPolicy : IPooledObjectPolicy<IComponent>
#else
		class PoolPolicy : ObjectPool<IComponent>.Policy
#endif
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
				if (obj is IResetable resetable)
					resetable.Reset();
				if (obj is IDisposable disposable)
					disposable.Dispose();
				if (obj is IModifiable modifiable)
					modifiable.modified = false;
				if (obj is IEntityIdRef entityIdRef)
					entityIdRef.entityId = 0;

				return true;
			}

			public void Dispose(IComponent obj)
			{
			}
		}
	}

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

	static class ComponentPoolFactory
	{
		public static IComponentPool Create(Type objType, int maxRetained = 0)
		{
			if (!typeof(IComponent).IsAssignableFrom(objType))
				throw new ArgumentException($"{objType.FullName} isn't IComponent");

			if (ComponentTrait.IsZeroSize(objType))
				return new ZeroSizeComponentPool(objType);
			else
				return new ComponentPool(objType, maxRetained);
		}
	}

	static class ComponentTrait
	{
		public static bool IsZeroSize<T>() where T : class, IComponent
		{
			return IsZeroSize(typeof(T));
		}

		public static bool IsZeroSize(Type type)
		{
			for (; ; )
			{
				if (type == null || type == typeof(object))
					return true;

				var members = type.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
				if (members.Any(m => m.MemberType != MemberTypes.Constructor))
					return false;

				type = type.BaseType;
			}
		}
	}
}
