using System.Collections.Generic;

namespace Entitas
{
	public class ContextGroupBuilder : IMatcherBuilder
	{
		private readonly Context _context;
		private readonly MatcherBuilder _builder = new MatcherBuilder();

		internal ContextGroupBuilder(Context c) => _context = c;

		public Group Result() => _context.GetGroup(_builder.Result());

		public MatcherBuilder AllOf(IReadOnlyList<int> indices) => _builder.AllOf(indices);
		public MatcherBuilder AnyOf(IReadOnlyList<int> indices) => _builder.AnyOf(indices);
		public MatcherBuilder NoneOf(IReadOnlyList<int> indices) => _builder.NoneOf(indices);

		public MatcherBuilder AllOf(params int[] indices) => _builder.AllOf(indices);
		public MatcherBuilder AnyOf(params int[] indices) => _builder.AnyOf(indices);
		public MatcherBuilder NoneOf(params int[] indices) => _builder.NoneOf(indices);
	}
}
