using System.Collections.Generic;
using Entitas.Utils;

namespace Entitas
{
	public class MatcherBuilder : IMatcherBuilder
	{
		private List<int> _allOfIndices;
		private List<int> _anyOfIndices;
		private List<int> _noneOfIndices;

		private Matcher _result;

		public Matcher Result() => _result ?? (_result = new Matcher(_allOfIndices, _anyOfIndices, _noneOfIndices));

		public void AllOf(IReadOnlyList<int> indices) => MakeIndices(ref _allOfIndices, indices);
		public void AnyOf(IReadOnlyList<int> indices) => MakeIndices(ref _anyOfIndices, indices);
		public void NoneOf(IReadOnlyList<int> indices) => MakeIndices(ref _noneOfIndices, indices);

		public void AllOf(params int[] indices) => AllOf((IReadOnlyList<int>)indices);
		public void AnyOf(params int[] indices) => AnyOf((IReadOnlyList<int>)indices);
		public void NoneOf(params int[] indices) => NoneOf((IReadOnlyList<int>)indices);

		private void MakeIndices(ref List<int> list, IReadOnlyList<int> indices)
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

			_result = null;
		}
	}
}
