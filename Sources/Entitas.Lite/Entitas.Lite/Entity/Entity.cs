using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Entitas.Utils;

namespace Entitas
{
	/// Use context.CreateEntity() to create a new entity and entity.Destroy() to destroy it.
	/// You can add and remove IComponent to an entity.
	public class Entity
	{
		/*
		/// Occurs when a component gets added.
        /// All event handlers will be removed when
        /// the entity gets destroyed by the context.
        public event EntityComponentChanged OnComponentAdded;

        /// Occurs when a component gets removed.
        /// All event handlers will be removed when
        /// the entity gets destroyed by the context.
        public event EntityComponentChanged OnComponentRemoved;

        /// Occurs when a component gets replaced.
        /// All event handlers will be removed when
        /// the entity gets destroyed by the context.
        public event EntityComponentReplaced OnComponentReplaced;

        /// Occurs when an entity gets released and is not retained anymore.
        /// All event handlers will be removed when
        /// the entity gets destroyed by the context.
        public event EntityEvent OnEntityReleased;

		/// Occurs when calling entity.Destroy().
		/// All event handlers will be removed when
		/// the entity gets destroyed by the context.
		public event EntityEvent OnDestroyEntity;
		*/

		/// The total amount of components an entity can possibly have.
		//public int totalComponents => _totalComponents;

		/// Each entity has its own unique id which will be set by the context when you create the entity.
		public int id => _id;
		public string name { get => _name; set { _name = value; _toStringCache = null; } }

		public bool isEnabled => _enabled;
		public bool isModified => _modified;

		/// The contextInfo is set by the context which created the entity and
		/// contains information about the context.
		/// It's used to provide better error messages.
		//public ContextInfo contextInfo => _contextInfo;

		private int _id;
		private string _name;
		private bool _enabled;
		private bool _modified;

		private IComponent[] _components;

		private IComponentPool[] _componentPools;
		private ContextInfo _contextInfo;
		private int _totalComponents;

		private object _syncObj = new object();

		internal Entity()
		{
		}

		internal void SetProvider(ContextInfo contextInfo, IComponentPool[] componentPools)
		{
			if (_contextInfo != contextInfo)
			{
				_contextInfo = contextInfo;
				_componentPools = componentPools;
				_totalComponents = contextInfo.GetComponentCount();

				_components = new IComponent[_totalComponents];
			}
		}
		
		internal void Active(int id, string name = null)
		{
			lock (_syncObj)
			{
				_id = id;
				_name = name;

				_enabled = true;
				_modified = true;
			}
		}

		/// Adds a component at the specified index.
		/// If already exists, return the old component.
		public IComponent AddComponent(int index)
		{
			lock (_syncObj)
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

				return component;
			}
		}

		public void RemoveComponent(int index)
		{
			lock (_syncObj)
			{
				if (!_enabled)
					return;

				RemoveComponentImpl(index);

				_modified = true;
			}
		}

		private void RemoveComponentImpl(int index)
		{
			var component = _components[index];
			if (component == null)
				return;

			_components[index] = null;
			_componentPools[index].Return(component);
		}

		/// Returns a component at the specified index for modification. Modified flag is set automatically.
		public IComponent ModifyComponent(int index)
		{
			var component = GetComponent(index);
			component?.Modify();

			return component;
		}

		public IComponent GetComponent(int index)
		{
			if (index < 0)
				return null;

			return _components[index];
		}

		public bool HasComponent(int index)
		{
            return _components[index] != null;
		}

		internal bool HasAllComponents(IReadOnlyList<int> indices)
		{
			for (int i = 0; i < indices.Count; i++)
			{
				if (_components[indices[i]] == null)
					return false;
			}

			return true;
		}

		internal bool HasAnyComponent(IReadOnlyList<int> indices)
		{
			for (int i = 0; i < indices.Count; i++)
			{
				if (_components[indices[i]] != null)
					return true;
			}

			return false;
		}

		public void Destroy()
		{
			_enabled = false;
		}

		internal void InternalDestroy()
		{
			lock (_syncObj)
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

