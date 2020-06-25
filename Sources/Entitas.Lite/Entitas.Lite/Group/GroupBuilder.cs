using System.Collections.Generic;

namespace Entitas
{
	public class GroupBuilder : IFilter
	{
		private readonly Context _context;
		private readonly MatcherBuilder _builder = new MatcherBuilder();

		public Group Result() => _context.GetGroup(_builder.Result());

		public void AllOf(IReadOnlyList<int> indices) => _builder.AllOf(indices);
		public void AnyOf(IReadOnlyList<int> indices) => _builder.AnyOf(indices);
		public void NoneOf(IReadOnlyList<int> indices) => _builder.NoneOf(indices);

		public void AllOf(params int[] indices) => _builder.AllOf(indices);
		public void AnyOf(params int[] indices) => _builder.AnyOf(indices);
		public void NoneOf(params int[] indices) => _builder.NoneOf(indices);

		internal GroupBuilder(Context c) => _context = c;

		public static implicit operator Group(GroupBuilder groupBuilder) => groupBuilder.Result();
	}

	public static class ContextGroupBuilderExtension
	{
		public static GroupBuilder BuildGroup(this Context c) => new GroupBuilder(c);
	}
}
