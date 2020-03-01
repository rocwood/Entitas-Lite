using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Entitas
{
	/// Automatic Entity Reference Counting (AERC)
	/// is used internally to prevent pooling retained entities.
	/// If you use retain manually you also have to
	/// release it manually at some point.
	/// SafeAERC checks if the entity has already been
	/// retained or released. It's slower, but you keep the information
	/// about the owners.
	public sealed class SafeAERC : IAERC
	{
		public int retainCount => _owners.Count;

		private readonly IEntity _entity;
		private readonly ConcurrentDictionary<object, bool> _owners = new ConcurrentDictionary<object, bool>();

		public ICollection<object> owners => _owners.Keys;

		public SafeAERC(IEntity entity)
		{
			_entity = entity;
		}

		public void Retain(object owner)
		{
			if (!_owners.TryAdd(owner, true))
				throw new EntityIsAlreadyRetainedByOwnerException(_entity, owner);
		}

		public void Release(object owner)
		{
			if (!_owners.TryRemove(owner, out bool val))
				throw new EntityIsNotRetainedByOwnerException(_entity, owner);
		}
	}
}
