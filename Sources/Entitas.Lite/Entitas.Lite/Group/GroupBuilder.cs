using System.Collections.Generic;

namespace Entitas
{
	public struct GroupBuilder
	{
		private Context _context;
		private Matcher _matcher;

		internal GroupBuilder(Context context, Matcher matcher)
		{
			_context = context;
			_matcher = matcher;
		}

		public GroupBuilder WithAll(IReadOnlyList<int> indices)
		{
			if (_matcher.isSealed)
				return SpawnNew();

			_matcher.WithAll(indices);
			return this;
		}

		public GroupBuilder WithAny(IReadOnlyList<int> indices)
		{
			if (_matcher.isSealed)
				return SpawnNew();

			_matcher.WithAny(indices);
			return this;
		}

		public GroupBuilder WithNone(IReadOnlyList<int> indices)
		{
			if (_matcher.isSealed)
				return SpawnNew();

			_matcher.WithNone(indices);
			return this;
		}

		private GroupBuilder SpawnNew()
		{
			return new GroupBuilder(_context, _matcher.Clone());
		}

		public Group GetGroup()
		{
			return _context.GetGroup(_matcher.MakeSealed());
		}
	}
}
