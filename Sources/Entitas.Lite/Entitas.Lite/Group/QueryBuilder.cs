using System.Collections.Generic;

namespace Entitas
{
	public struct QueryBuilder
	{
		private Context _context;
		private Query _query;

		internal QueryBuilder(Context context, Query result)
		{
			_context = context;
			_query = result;
		}

		public QueryBuilder WithAll(IReadOnlyList<int> indices)
		{
			if (_query.isSealed)
				return SpawnNew();

			_query.WithAll(indices);
			return this;
		}

		public QueryBuilder WithAny(IReadOnlyList<int> indices)
		{
			if (_query.isSealed)
				return SpawnNew();

			_query.WithAny(indices);
			return this;
		}

		public QueryBuilder WithNone(IReadOnlyList<int> indices)
		{
			if (_query.isSealed)
				return SpawnNew();

			_query.WithNone(indices);
			return this;
		}

		private QueryBuilder SpawnNew()
		{
			return new QueryBuilder(_context, _query.Clone());
		}

		public Group GetGroup()
		{
			return _context.GetGroup(_query.MakeSealed());
		}

		/*
		public IReadOnlyList<Entity> GetEntities()
		{
			var group = GetGroup();
			return group.GetEntities();
		}
		*/
	}
}
