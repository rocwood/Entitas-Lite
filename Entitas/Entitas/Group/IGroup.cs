using System.Collections;
using System.Collections.Generic;

namespace Entitas {

    public delegate void GroupChanged(IGroup group, Entity entity, int index, IComponent component);
    public delegate void GroupUpdated(IGroup group, Entity entity, int index, IComponent previousComponent, IComponent newComponent);

    public interface IGroup : IEnumerable<Entity> {

        int count { get; }

        void RemoveAllEventHandlers();

		event GroupChanged OnEntityAdded;
		event GroupChanged OnEntityRemoved;
		event GroupUpdated OnEntityUpdated;

		IMatcher matcher { get; }

		void HandleEntitySilently(Entity entity);
		void HandleEntity(Entity entity, int index, IComponent component);

		GroupChanged HandleEntity(Entity entity);

		void UpdateEntity(Entity entity, int index, IComponent previousComponent, IComponent newComponent);

		bool ContainsEntity(Entity entity);

		Entity[] GetEntities();
		Entity GetSingleEntity();
	}
}
