using System;
using System.Runtime.CompilerServices;


namespace Entitas
{
	interface IComponentPool
	{
		IComponent Get();
		void Return(IComponent obj);
	}

	class ComponentPool : IComponentPool
	{
		private readonly SimplePool<IComponent> _pool;
		private readonly Type _objType;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ComponentPool(Type objType, int maxRetained = 0)
		{
			_pool = new SimplePool<IComponent>(maxRetained);
			_objType = objType;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IComponent Get()
		{
			return _pool.Get() ?? (IComponent)Activator.CreateInstance(_objType);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Return(IComponent obj)
		{
			if (obj == null)
				return;

			if (obj is IAutoReset autoReset)
				autoReset.Reset();

			_pool.Return(obj);
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
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IComponentPool Create(Type objType, bool zeroSize, int maxRetained)
		{
			if (zeroSize)
				return new ZeroSizeComponentPool(objType);
			else
				return new ComponentPool(objType, maxRetained);
		}
	}
}
