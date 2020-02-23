using System;
using System.Collections.Generic;
using Entitas.Utils;

namespace Entitas
{
	public delegate void EntityComponentChanged(Entity entity, int index, IComponent component);
	//public delegate void EntityComponentReplaced(Entity entity, int index, IComponent previousComponent, IComponent newComponent);
	public delegate void EntityEvent(Entity entity);

	public interface IEntity
	{
		event EntityComponentChanged OnComponentAdded;
		event EntityComponentChanged OnComponentRemoved;
		//event EntityComponentReplaced OnComponentReplaced;
		//event EntityEvent OnEntityReleased;
		event EntityEvent OnDestroyEntity;

		int totalComponents { get; }
		int creationIndex { get; }
		int id { get; }
		bool isEnabled { get; }

		ContextInfo contextInfo { get; }

		/*
		void Initialize(int id,
						int totalComponents,
						Stack<IComponent>[] componentPools,
						ContextInfo contextInfo);
		*/
		//void Reactivate(int id);

		IComponent AddComponent(int index);
		void RemoveComponent(int index);
		//void ReplaceComponent(int index, IComponent component);

		IComponent GetComponent(int index);
		IComponent ModifyComponent(int index);
		//IComponent[] GetComponents();
		//int[] GetComponentIndices();

		bool HasComponent(int index);
		//bool HasAllComponents(BitArray mask);
		//bool HasAnyComponent(BitArray mask);

		void RemoveAllComponents();

		//IComponent CreateComponent(int index, Type type);

		void Destroy();

		//void InternalDestroy();
		//void RemoveAllOnEntityReleasedHandlers();
	}
}
