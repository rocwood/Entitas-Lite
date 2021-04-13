using System.Collections.Generic;

namespace Entitas
{
	public partial class Context
	{
		public static int defaultGroupCapacity = 64;

		private readonly Dictionary<Matcher, Group> _groupLookup = new Dictionary<Matcher, Group>(defaultGroupCapacity);
		private readonly List<Group> _groupList = new List<Group>(defaultGroupCapacity);

		public Group GetGroup(Matcher matcher)
		{
			if (!_groupLookup.TryGetValue(matcher, out var group))
			{
				group = new Group(matcher);

				//for (int i = 0; i < _entities.Count; i++)
				foreach (var entity in _entities)
				{
					//var entity = _entities[i];

					if (entity.isEnabled)
						group.HandleEntity(entity);
				}

				_groupLookup.Add(matcher, group);
				_groupList.Add(group);
			}

			return group;
		}

		private void HandleGroupChanges()
		{
			var modifiedEntities = _modifiedEntities.Values;
			if (modifiedEntities.Count <= 0)
				return;

			//for (int i = 0; i < _entities.Count; i++)
			//{
			//	var entity = _entities[i];

			foreach (var entity in modifiedEntities)
			{
				if (entity.isModified || !entity.isEnabled)
				{
					for (int j = 0; j < _groupList.Count; j++)
						_groupList[j].HandleEntity(entity);

					entity.ResetModified();
				}
			}

			//_entities.ResetModifiedEntities();
		}

		private void ClearGroups()
		{
			_groupList.Clear();
			_groupLookup.Clear();
		}
	}
}
