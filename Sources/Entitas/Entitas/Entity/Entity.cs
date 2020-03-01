using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Entitas.Utils;

namespace Entitas
{
	/// Use context.CreateEntity() to create a new entity and
	/// entity.Destroy() to destroy it.
	/// You can add, replace and remove IComponent to an entity.
	public partial class Entity : IEntity
	{
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
		//public event EntityComponentReplaced OnComponentReplaced;

		/// Occurs when an entity gets released and is not retained anymore.
		/// All event handlers will be removed when
		/// the entity gets destroyed by the context.
		public event EntityEvent OnEntityReleased;

		/// Occurs when calling entity.Destroy().
		/// All event handlers will be removed when
		/// the entity gets destroyed by the context.
		public event EntityEvent OnDestroyEntity;

		/// The total amount of components an entity can possibly have.
		public int totalComponents => _totalComponents;

		/// Each entity has its own unique creationIndex which will be set by
		/// the context when you create the entity.
		public int creationIndex => _creationIndex;
		public int id => _creationIndex;

		/// The context manages the state of an entity.
		/// Active entities are enabled, destroyed entities are not.
		public bool isEnabled => _isEnabled;

		/// Optional name
		public string name { get => _name; set { _name = value; _toStringCache = null; } }

		/// The contextInfo is set by the context which created the entity and
		/// contains information about the context.
		/// It's used to provide better error messages.
		public ContextInfo contextInfo => _contextInfo;

		/// Automatic Entity Reference Counting (AERC)
		/// is used internally to prevent pooling retained entities.
		/// If you use retain manually you also have to
		/// release it manually at some point.
		public IAERC aerc { get { return _aerc; } }

		int _creationIndex;
		bool _isEnabled;
		String _name;

		IComponent[] _components;
		IComponentPool[] _componentPools;
		ContextInfo _contextInfo;
		int _totalComponents;

		IAERC _aerc;

		IComponent[] _componentsCache;
		//int[] _componentIndicesCache;
		string _toStringCache;
		//StringBuilder _toStringBuilder;

		readonly object _syncObj = new object();

		internal Entity()
		{
		}

		internal void Initialize(int creationIndex, IComponentPool[] componentPools, ContextInfo contextInfo, IAERC aerc)
		{
			Reactivate(creationIndex);

			_totalComponents = contextInfo.GetComponentCount();
			_components = new IComponent[totalComponents];
			_componentPools = componentPools;

			_contextInfo = contextInfo;
			_aerc = aerc;
		}

		internal void Reactivate(int creationIndex)
		{
			_creationIndex = creationIndex;
			_isEnabled = true;
		}

		/*
		/// Adds a component at the specified index.
		/// You can only have one component at an index.
		/// Each component type must have its own constant index.
		/// The prefered way is to use the
		/// generated methods from the code generator.
		public void AddComponent(int index, IComponent component)
		{
			if (!_isEnabled)
			{
				throw new EntityIsNotEnabledException(
					"Cannot add component '" +
					_contextInfo.componentNames[index] + "' to " + this + "!"
				);
			}

			if (HasComponent(index))
			{
				throw new EntityAlreadyHasComponentException(
					index, "Cannot add component '" +
					_contextInfo.componentNames[index] + "' to " + this + "!",
					"You should check if an entity already has the component " +
					"before adding it or use entity.ReplaceComponent()."
				);
			}

			var entityIdRef = component as IEntityIdRef;
			if (entityIdRef != null)
				entityIdRef.entityId = _creationIndex;

			var modifiable = component as IModifiable;
			if (modifiable != null)
				modifiable.modified = true;

			_components[index] = component;
			_componentsCache = null;
			_componentIndicesCache = null;
			//_toStringCache = null;

			if (OnComponentAdded != null)
			{
				OnComponentAdded(this, index, component);
			}
		}
		*/

		/// Adds a component at the specified index.
		/// If already exists, return the old component.
		public IComponent AddComponent(int index)
		{
			lock (_syncObj)
			{
				if (!_isEnabled)
					throw new EntityIsNotEnabledException($"Cannot add component {_contextInfo.componentNames[index]} to {this} !");

				// If exists, return the old component
				var component = _components[index];
				if (component == null)
				{
					component.Modify();
					return component;
				}

				component = _componentPools[index].Get();
				component.SetEntityId(_creationIndex);
				component.Modify();

				_components[index] = component;
				_componentsCache = null;
				//_componentIndicesCache = null;

				OnComponentAdded?.Invoke(this, index, component);

				return component;
			}
		}

		/// Removes a component at the specified index if exists.
		public void RemoveComponent(int index)
		{
			lock (_syncObj)
			{
				if (!_isEnabled)
					throw new EntityIsNotEnabledException($"Cannot remove component {_contextInfo.componentNames[index]} from {this} !");

				DoRemoveComponent(index);
			}
		}

		private void DoRemoveComponent(int index)
		{
			var component = _components[index];
			if (component == null)
				return;

			_components[index] = null;
			_componentsCache = null;

			OnComponentRemoved?.Invoke(this, index, component);

			_componentPools[index].Return(component);
		}

		/*
		void replaceComponent(int index, IComponent replacement)
		{
			//_toStringCache = null;
			var previousComponent = _components[index];
			if (replacement != previousComponent)
			{
				_components[index] = replacement;
				_componentsCache = null;
				if (replacement != null)
				{
					if (OnComponentReplaced != null)
					{
						OnComponentReplaced(this, index, previousComponent, replacement);
					}
				}
				else
				{
					_componentIndicesCache = null;
					if (OnComponentRemoved != null)
					{
						OnComponentRemoved(this, index, previousComponent);
					}
				}

				// Reset before return to pool
				var resetablePrevComponent = previousComponent as IResetable;
				if (resetablePrevComponent != null)
				{
					resetablePrevComponent.Reset();
				}

				// Reset entityId
				var entityIdRef = previousComponent as IEntityIdRef;
				if (entityIdRef != null)h
					entityIdRef.entityId = 0;

				// Reset modified flag
				var modifiable = previousComponent as IModifiable;
				if (modifiable != null)
					modifiable.modified = false;

				_componentPools[index].Return(previousComponent);
			}
			else
			{
				if (OnComponentReplaced != null)
				{
					OnComponentReplaced(this, index, previousComponent, replacement);
				}
			}
		}

		/// Returns a component at the specified index for modification.
		/// Mark changes automatically.
		public IComponent ModifyComponent(int index)
		{
			var component = GetComponent(index);
			component?.Modify();

			return component;
		}
		*/

		/// Returns a component at the specified index.
		/// You can only get a component at an index if it exists.
		public IComponent GetComponent(int index)
		{
			if (index < 0)
				return null;

			lock (_syncObj)
			{
				return _components[index];
			}
		}

		/// Returns all added components.
		public IComponent[] GetComponents()
		{
			lock (_syncObj)
			{
				if (_componentsCache == null)
				{
					_componentsCache = new IComponent[_totalComponents];
					_components.CopyTo(_componentsCache, 0);
				}

				return _componentsCache;
			}
		}

		/*
		/// Returns all indices of added components.
		public int[] GetComponentIndices()
		{
			if (_componentIndicesCache == null)
			{
				var indices = EntitasCache.GetIntList();

				for (int i = 0; i < _components.Length; i++)
				{
					if (_components[i] != null)
					{
						indices.Add(i);
					}
				}

				_componentIndicesCache = indices.ToArray();

				EntitasCache.PushIntList(indices);
			}

			return _componentIndicesCache;
		}
		*/

		/// Determines whether this entity has a component
		/// at the specified index.
		public bool HasComponent(int index)
		{
			if (index < 0)
				return false;

			return _components[index] != null;
		}

		/// Determines whether this entity has components
		/// at all the specified indices.
		public bool HasComponents(int[] indices)
		{
			for (int i = 0; i < indices.Length; i++)
			{
				if (_components[indices[i]] == null)
					return false;
			}

			return true;
		}

		/// Determines whether this entity has a component
		/// at any of the specified indices.
		public bool HasAnyComponent(int[] indices)
		{
			for (int i = 0; i < indices.Length; i++)
			{
				if (_components[indices[i]] != null)
					return true;
			}

			return false;
		}

		/// Removes all components.
		public void RemoveAllComponents()
		{
			lock (_syncObj)
			{
				// Don't check enabled 
				//if (!_isEnabled)
				//	throw new EntityIsNotEnabledException($"Cannot remove all components from {this} !");

				for (int i = 0; i < _components.Length; i++)
					DoRemoveComponent(i);
			}
		}

		/// Returns the number of objects that retain this entity.
		public int retainCount => _aerc.retainCount;

		/// Retains the entity. An owner can only retain the same entity once.
		/// Retain/Release is part of AERC (Automatic Entity Reference Counting)
		/// and is used internally to prevent pooling retained entities.
		/// If you use retain manually you also have to
		/// release it manually at some point.
		public void Retain(object owner)
		{
			_aerc.Retain(owner);
		}

		/// Releases the entity. An owner can only release an entity
		/// if it retains it.
		/// Retain/Release is part of AERC (Automatic Entity Reference Counting)
		/// and is used internally to prevent pooling retained entities.
		/// If you use retain manually you also have to
		/// release it manually at some point.
		public void Release(object owner)
		{
			_aerc.Release(owner);

			if (_aerc.retainCount == 0)
				OnEntityReleased?.Invoke(this);
		}

		// Dispatches OnDestroyEntity which will start the destroy process.
		public void Destroy()
		{
			lock (_syncObj)
			{
				if (!_isEnabled)
					throw new EntityIsNotEnabledException($"Cannot destroy {this} !");

				OnDestroyEntity?.Invoke(this);
			}
		}

		// This method is used internally. Don't call it yourself.
		// Use entity.Destroy();
		internal void InternalDestroy()
		{
			_isEnabled = false;
			_name = null;
			_toStringCache = null;

			RemoveAllComponents();

			OnComponentAdded = null;
			//OnComponentReplaced = null;
			OnComponentRemoved = null;
			OnDestroyEntity = null;
		}

		// Do not call this method manually. This method is called by the context.
		internal void RemoveAllOnEntityReleasedHandlers()
		{
			OnEntityReleased = null;
		}

		/// Returns a cached string to describe the entity
		/// with the following format:
		/// Entity({creationIndex}) {name}
		public override string ToString()
		{
			if (_toStringCache == null)
				_toStringCache = $"Entity({_creationIndex}) {_name}";

			return _toStringCache;
		}
	}
}
