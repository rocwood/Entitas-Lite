using System.Linq;

namespace Entitas
{
	public class GroupSingleEntityException : EntitasException
	{
		public GroupSingleEntityException(IGroup group)
			: base($"Cannot get the single entity from {group}!\nGroup contains {group.count} entities:",
					string.Join("\n", group.GetEntities().Select(e => e.ToString()).ToArray()))
		{
		}
	}

	public class GroupContainsConflictEntityIdException : EntitasException
	{
		public GroupContainsConflictEntityIdException(IGroup group, Entity entity)
			: base($"Group {group} contains conflict Entity.id = {entity.id}")
		{
		}
	}

}
