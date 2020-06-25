using System.Collections.Generic;

namespace Entitas
{
	public interface IMatcherBuilder
	{
		MatcherBuilder AllOf(IReadOnlyList<int> indices);
		MatcherBuilder AnyOf(IReadOnlyList<int> indices);
		MatcherBuilder NoneOf(IReadOnlyList<int> indices);

		MatcherBuilder AllOf(params int[] indices);
		MatcherBuilder AnyOf(params int[] indices);
		MatcherBuilder NoneOf(params int[] indices);
	}
}
