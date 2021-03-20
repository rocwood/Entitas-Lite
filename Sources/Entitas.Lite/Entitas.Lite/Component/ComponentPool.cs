using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.ObjectPool;

namespace Entitas
{
	public interface IComponentPool
	{
		IComponent Get();
		void Return(IComponent obj);
	}

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
