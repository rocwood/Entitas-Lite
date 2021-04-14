using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Entitas
{
	public partial class Context
	{
		public static int defaultQueryCapacity = 64;

		private readonly List<EntityQuery> _queryList = new List<EntityQuery>(defaultQueryCapacity);
		private readonly Dictionary<Type, EntityQuery> _queryLookup = new Dictionary<Type, EntityQuery>(defaultQueryCapacity);

		private readonly object[] _createQueryParams;


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetGroup<T>(out T query) where T : EntityQuery
		{
			query = (T)GetQuery(typeof(T));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T GetGroup<T>() where T : EntityQuery
		{
			return (T)GetQuery(typeof(T));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal EntityQuery GetQuery(Type queryType)
		{
			if (!_queryLookup.TryGetValue(queryType, out var query))
			{
				if (!typeof(EntityQuery).IsAssignableFrom(queryType))
					return null;

				query = (EntityQuery)Activator.CreateInstance(queryType, BindingFlags.NonPublic|BindingFlags.Instance, null, _createQueryParams, CultureInfo.InvariantCulture);
				if (query == null)
					return null;

				foreach (var entity in _entities)
				{
					if (entity.isEnabled)
						query.InitWithEntity(entity);
				}

				_queryLookup.Add(queryType, query);
				_queryList.Add(query);
			}

			return query;
		}

		private void ClearGroups()
		{
			_queryList.Clear();
			_queryLookup.Clear();
		}
	}
}
