using System.Runtime.CompilerServices;

namespace Entitas
{
	public partial class Entity
	{
		public int id
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _id;
		}
		
		public string name 
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _name;

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _name = value;
		}

		public bool isEnabled
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _enabled;
		}
		
		public bool isModified
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _modified;
		}

		private int _id;
		private string _name;

		private bool _enabled;
		private bool _modified;

		private readonly IComponent[] _components;
		private readonly IComponentPool[] _componentPools;
		private readonly Context _owner;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Entity(Context owner, IComponentPool[] componentPools)
		{
			_owner = owner;

			_componentPools = componentPools;
			_components = new IComponent[_componentPools.Length];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void Activate(int id, string name)
		{
			//lock (_syncObj)
			{
				_id = id;
				_name = name;
				_enabled = true;

				//SetModified();
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private IComponent AddComponent(int index)
		{
			//lock (_syncObj)
			{
				if (!_enabled)
					return null;

				// If exists, return the old component
				var component = _components[index];
				if (component != null)
				{
					component.Modify();
					return component;
				}

				// add from pool
				_components[index] = component = _componentPools[index].Get();
				component.Modify();

				SetModified();

				return component;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void RemoveComponent(int index)
		{
			//lock (_syncObj)
			{
				if (!_enabled)
					return;

				RemoveComponentImpl(index);

				SetModified();
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void RemoveComponentImpl(int index)
		{
			var component = _components[index];
			if (component == null)
				return;

			_components[index] = null;
			_componentPools[index].Return(component);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private IComponent ModifyComponent(int index)
		{
			var component = GetComponent(index);
			component?.Modify();

			return component;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private IComponent GetComponent(int index)
		{
			return _components[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool HasComponent(int index)
		{
            return _components[index] != null;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetModified()
		{
			_modified = true;
			_owner.MarkModified(this);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ResetModified()
		{
			_modified = false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Destroy()
		{
			SetModified();

			_enabled = false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void DestroyImpl()
		{
			//lock (_syncObj)
			{
				for (int i = 0; i < _components.Length; i++)
					RemoveComponentImpl(i);

				_modified = false;
				_enabled = false;

				_id = 0;
				_name = null;
			}
		}
	}
}

