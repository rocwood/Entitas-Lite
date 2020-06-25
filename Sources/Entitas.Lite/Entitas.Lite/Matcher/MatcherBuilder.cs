using System.Collections.Generic;
using Entitas.Utils;

namespace Entitas
{
	public class MatcherBuilder : IMatcherBuilder
	{
		public Matcher Result() => new Matcher(_allOfIndices, _anyOfIndices, _noneOfIndices);

		public MatcherBuilder AllOf(IReadOnlyList<int> indices)
		{
			MakeList(ref _allOfIndices, indices);
			return this;
		}

		public MatcherBuilder AnyOf(IReadOnlyList<int> indices)
		{
			MakeList(ref _anyOfIndices, indices);
			return this;
		}

		public MatcherBuilder NoneOf(IReadOnlyList<int> indices)
		{
			MakeList(ref _noneOfIndices, indices);
			return this;
		}

		public MatcherBuilder AllOf(params int[] indices) => AllOf((IReadOnlyList<int>)indices);
		public MatcherBuilder AnyOf(params int[] indices) => AnyOf((IReadOnlyList<int>)indices);
		public MatcherBuilder NoneOf(params int[] indices) => NoneOf((IReadOnlyList<int>)indices);

		private static void MakeList(ref List<int> list, IReadOnlyList<int> indices)
		{
			if (indices == null)
			{
				list = null;
			}
			else
			{
				if (list == null)
					list = new List<int>(indices.Count);

				list.Clear();
				list.AddDistinctSorted(indices);
			}
		}

		private List<int> _allOfIndices;
		private List<int> _anyOfIndices;
		private List<int> _noneOfIndices;
	}
}
