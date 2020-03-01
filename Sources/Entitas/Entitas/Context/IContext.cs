namespace Entitas
{
	public delegate void ContextEntityChanged(IContext context, Entity entity);
	public delegate void ContextGroupChanged(IContext context, IGroup group);

	public interface IContext
	{
		event ContextEntityChanged OnEntityCreated;
		event ContextEntityChanged OnEntityWillBeDestroyed;
		event ContextEntityChanged OnEntityDestroyed;

		event ContextGroupChanged OnGroupCreated;

		int totalComponents { get; }

		ContextInfo contextInfo { get; }

		int count { get; }
		int reusableEntitiesCount { get; }
		int retainedEntitiesCount { get; }

		void DestroyAllEntities();

		void Reset();

		Entity CreateEntity();

		bool HasEntity(Entity entity);
		Entity[] GetEntities();

		IGroup GetGroup(IMatcher matcher);
	}
}
