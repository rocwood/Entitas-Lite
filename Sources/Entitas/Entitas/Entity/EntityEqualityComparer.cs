using System.Collections.Generic;

namespace Entitas
{
	public class EntityEqualityComparer : IEqualityComparer<Entity>
	{
		public static readonly IEqualityComparer<Entity> comparer = new EntityEqualityComparer();

		public bool Equals(Entity x, Entity y) => x == y;

		public int GetHashCode(Entity obj) => obj.creationIndex;
	}
}
