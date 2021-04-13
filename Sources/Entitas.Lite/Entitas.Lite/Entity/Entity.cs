using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Entitas.Utils;

namespace Entitas
{
	public class Entity
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
			set { _name = value; _toStringCache = null; } 
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

		//private readonly Context owner;

		private Dictionary<int, Entity> _modifiedSet;

		//private object _syncObj = new object();


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Entity(Context owner, IComponentPool[] componentPools, Dictionary<int, Entity> modifiedSet)
		{
			//this.owner = owner;

			_componentPools = componentPools;
			_components = new IComponent[_componentPools.Length];

			_modifiedSet = modifiedSet;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void Active(int id, string name = null)
		{
			//lock (_syncObj)
			{
				_id = id;
				_name = name;

				_enabled = true;
				_modified = true;
				_modifiedSet[_id] = this;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IComponent AddComponent(int index)
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

				_modified = true;
				_modifiedSet[_id] = this;

				return component;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void RemoveComponent(int index)
		{
			//lock (_syncObj)
			{
				if (!_enabled)
					return;

				RemoveComponentImpl(index);

				_modified = true;
				_modifiedSet[_id] = this;
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
		public IComponent ModifyComponent(int index)
		{
			var component = GetComponent(index);
			component?.Modify();

			return component;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IComponent GetComponent(int index)
		{
			return _components[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool HasComponent(int index)
		{
            return _components[index] != null;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool HasAllComponents(IReadOnlyList<int> indices)
		{
			for (int i = 0; i < indices.Count; i++)
			{
				if (_components[indices[i]] == null)
					return false;
			}

			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool HasAnyComponent(IReadOnlyList<int> indices)
		{
			for (int i = 0; i < indices.Count; i++)
			{
				if (_components[indices[i]] != null)
					return true;
			}

			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ResetModified()
		{
			_modified = false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Destroy()
		{
			_enabled = false;
			_modified = true;
			_modifiedSet[_id] = this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void InternalDestroy()
		{
			//lock (_syncObj)
			{
				for (int i = 0; i < _components.Length; i++)
					RemoveComponentImpl(i);

				_modified = false;
				_enabled = false;

				_id = 0;
				_name = null;
				_toStringCache = null;
			}
		}

		private string _toStringCache;

		public override string ToString()
		{
			if (_toStringCache == null)
				_toStringCache = $"Entity({_id}) {_name}";

			return _toStringCache;
		}
	}
}

